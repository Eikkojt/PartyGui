using System.Drawing.Printing;
using System.Net;
using Downloader;
using HtmlAgilityPack;
using PartyLib.Bases;
using RestSharp;
using RandomUserAgent;
using PartyLib.Config;
using PartyLib.Helpers;
using System.ComponentModel;

// ReSharper disable PossibleLossOfFraction

namespace PartyLib;

public class ScraperFunctions
{
    /// <summary>
    /// Collection of headers for the downloader to use
    /// </summary>
    private readonly WebHeaderCollection downloadHeaders = new();

    /// <summary>
    /// Class constructor
    /// </summary>
    /// <param name="creatorIn">Creator that the functions will be used on</param>
    /// <param name="totalNumberOfRequestedPosts">
    /// How many posts this functions class will be working with
    /// </param>
    public ScraperFunctions(Creator creatorIn, int totalNumberOfRequestedPosts)
    {
        Creator = creatorIn;
        TotalRequestedPosts = totalNumberOfRequestedPosts;
    }

    /// <summary>
    /// Creator class for various informations
    /// </summary>
    public Creator Creator { get; }

    /// <summary>
    /// Total number of posts the user has requested
    /// </summary>
    public int TotalRequestedPosts { get; set; }

    /// <summary>
    /// Raw download function for interacting with the downloader library
    /// </summary>
    /// <param name="url"></param>
    /// <param name="folder"></param>
    /// <param name="filename"></param>
    /// <returns>The status of the download</returns>
    private DownloadStatus RawDownloadBuilder(string url, string folder, string filename)
    {
        // Headers
        //downloadHeaders.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
        //downloadHeaders.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");

        // Random user agent
        string userAgent = RandomUa.RandomUserAgent;

        // Downloader options
        var downloadOpt = new DownloadConfiguration
        {
            ChunkCount = PartyConfig.DownloadFileParts,
            ParallelDownload = true,
            ParallelCount = PartyConfig.ParallelDownloadParts,
            MaxTryAgainOnFailover = 5,
            RequestConfiguration =
            {
                Accept = "*/*",
                Headers = downloadHeaders,
                KeepAlive = true,
                ProtocolVersion = HttpVersion.Version11,
                UseDefaultCredentials = false,
                UserAgent = userAgent
            }
        };
        IDownload download = DownloadBuilder.New()
            .WithUrl(url)
            .WithDirectory(folder)
            .WithFileName(filename)
            .WithConfiguration(downloadOpt)
            .Build();
        download.DownloadFileCompleted += (sender, e) => Downloader_DownloadFileCompleted(sender, e, filename);
        download.StartAsync().Wait();
        return download.Status;
    }

    /// <summary>
    /// Function called when a downloaded file has completed downloading, either with a failiure or
    /// with a completed file
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="fileName"></param>
    private async void Downloader_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e, string fileName)
    {
        if (e.Error != null)
        {
            PartyConfig.DownloaderErrors.Add(Tuple.Create(e.Error, fileName));
        }
    }

    /// <summary>
    /// Downloads content from a specified raw media URL
    /// </summary>
    /// <param name="url"></param>
    /// <param name="parentFolder"></param>
    /// <param name="fileName"></param>
    /// <returns>A boolean on whether the download was successful</returns>
    private bool DownloadContent(string? url, string parentFolder, string? fileName)
    {
        string sanitizedFileName = Strings.SanitizeFile(fileName);

        // Make parent folder
        if (!Directory.Exists(parentFolder))
        {
            Directory.CreateDirectory(parentFolder);
        }

        DownloadStatus status;
        try
        {
            if (File.Exists(parentFolder + "/" + sanitizedFileName))
            {
                status = RawDownloadBuilder(url, parentFolder, MathHelper.RandomGenerator.Next(1, 999) + "-" + sanitizedFileName);
            }
            else
            {
                status = RawDownloadBuilder(url, parentFolder, sanitizedFileName);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception \"" + ex + "\" occurred while downloading " + sanitizedFileName + "!");
            status = DownloadStatus.Failed;
        }

        if (status == DownloadStatus.Completed)
        {
            return true;
        }
        else
        {
            Console.WriteLine($"Download {fileName} failed! Skipping...");
            return false;
        }
    }

    /// <summary>
    /// Scrapes a post and returns an assembled class
    /// </summary>
    /// <param name="postUrl"></param>
    /// <param name="iteration">How many posts back from the most recent post this post is</param>
    private Post ScrapePost(string postUrl, int iteration)
    {
        var post = new Post(postUrl, Creator);
        post.Iteration = iteration;
        post.ReverseIteration = TotalRequestedPosts - (iteration - 1);
        post.URL = postUrl;

        var response = HttpHelper.HttpGet(new RestRequest(), postUrl);
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(response.Content);

        return post;
    }

    /// <summary>
    /// Scrapes a creator's page for posts.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="numberOfPostsToGet"></param>
    /// <returns>A list of posts</returns>
    public List<Post> ScrapePage(int page, int numberOfPostsToGet)
    {
        var postUrls = new List<Post>();
        var request = new RestRequest().AddParameter("o", page * 50); // Page parameter calculation

        // HTTP query
        var response = HttpHelper.HttpGet(request, Creator.URL, true, true);
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(response.Content);

        // Parsing
        var postsContainer = htmlDoc.DocumentNode.Descendants().FirstOrDefault(x => x.HasClass("card-list__items") && x.Name == "div"); // Fetch the container that holds the posts
        var count = 0;
        if (postsContainer?.ChildNodes != null)
        {
            foreach (var post in postsContainer?.ChildNodes)
            {
                if (post.NodeType != HtmlNodeType.Element) // We don't want text or comments
                    continue;

                if (count >= numberOfPostsToGet) // We want to cut off the loop when the threshold is reached
                    break;

                var linkNode = post.ChildNodes.FirstOrDefault(x => x.Attributes["href"] != null); // Find first child node with a link attribute (only "a" nodes here)

                // Add URL to the list
                postUrls.Add(ScrapePost(Creator.PartyDomain + linkNode?.Attributes["href"].Value, (page * 50) + count));
                count++;
            }
        }

        return postUrls;
    }

    /// <summary>
    /// Downloads the specified attachment. Basically just a public wrapper.
    /// </summary>
    /// <param name="attachment"></param>
    /// <param name="parentFolder"></param>
    /// <returns>A boolean representing the success of the download</returns>
    public bool DownloadAttachment(Attachment attachment, string parentFolder)
    {
        if (attachment is { URL: not null, FileName: not null })
        {
            return DownloadContent(attachment.URL, parentFolder, attachment.FileName);
        }
        else
        {
            return false;
        }
    }
}