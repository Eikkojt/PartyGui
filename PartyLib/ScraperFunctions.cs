using System.Linq.Expressions;
using System.Net;
using Downloader;
using GTranslate.Translators;
using HtmlAgilityPack;
using PartyLib.Bases;
using RestSharp;
using RestSharp.Serializers.Json;

// ReSharper disable PossibleLossOfFraction

namespace PartyLib;

public class ScraperFunctions
{
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
    /// Does math to separate pages from a number of posts. 0 can be input to process all of the
    /// creator's posts
    /// </summary>
    /// <returns>A PageDetails class</returns>
    public PageDetails DoPageMath()
    {
        if (TotalRequestedPosts != 0)
        {
            // Posts integer is a defined value, so separation code goes here
            if (TotalRequestedPosts <= 50)
            {
                // Posts integer only requires 1 page
                return new PageDetails(0, TotalRequestedPosts, true);
            }
            else
            {
                // Posts integer requires more than 1 page
                return new PageDetails((int)Math.Floor((float)(TotalRequestedPosts / 50)), TotalRequestedPosts % 50, false);
            }
        }

        var totalPosts = Creator.GetTotalPosts();
        if (totalPosts != -1)
        {
            return new PageDetails((int)Math.Floor((float)(totalPosts / 50)), totalPosts % 50, false);
        }
        else
        {
            // Total posts element doesn't appear if there is only 1 page, so that logic is handled here
            var posts = new List<HtmlNode>();
            var postsList = Creator.LandingPage.DocumentNode.SelectSingleNode("/html/body/div[2]/main/section/div[3]/div[2]");
            foreach (var post in postsList.ChildNodes)
            {
                if (post.NodeType == HtmlNodeType.Element)
                {
                    posts.Add(post);
                }
            }
            return new PageDetails(0, posts.Count, true);
        }
    }

    /// <summary>
    /// Scrapes a creator's page for posts
    /// </summary>
    /// <param name="page"></param>
    /// <param name="numberOfPostsToGet"></param>
    /// <returns>A list of URLs to discovered posts</returns>
    public List<string> ScrapePage(int page, int numberOfPostsToGet)
    {
        var postUrls = new List<string>();
        var client = new RestClient(Creator.URL);
        var request = new RestRequest().AddParameter("o", page * 50); // Page parameter calculation

        // Headers
        request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
        request.AddHeader("Accept-Encoding", "gzip, deflate, br");
        request.AddHeader("Accept-Language", "en-US,en;q=0.5");
        request.AddHeader("Connection", "keep-alive");
        request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/119.0");
        var response = client.GetAsync(request).Result;
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(response.Content);

        var postsContainer = htmlDoc.DocumentNode.Descendants().FirstOrDefault(x => x.HasClass("card-list__items") && x.Name == "div"); // Fetch the container that holds the posts
        var count = 0;
        if (postsContainer?.ChildNodes != null)
            if (postsContainer?.ChildNodes != null)
                foreach (var post in postsContainer?.ChildNodes)
                {
                    if (post.NodeType != HtmlNodeType.Element) // We don't want text or comments
                        continue;
                    if (count >= numberOfPostsToGet) // We want to cut off the loop when the threshold is reached
                        break;
                    var linkNode =
                        post.ChildNodes.FirstOrDefault(x =>
                            x.Attributes["href"] !=
                            null); // Find first child node with a link attribute (only "a" nodes here)
                    postUrls.Add(Creator.PartyDomain + linkNode?.Attributes["href"].Value);
                    count++;
                }

        return postUrls;
    }

    private WebHeaderCollection downloadHeaders = new WebHeaderCollection();

    /// <summary>
    /// Downloads content from a specified raw media URL
    /// </summary>
    /// <param name="url"></param>
    /// <param name="parentFolder"></param>
    /// <param name="fileName"></param>
    private void DownloadContent(string? url, string parentFolder, string? fileName)
    {
        downloadHeaders.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
        downloadHeaders.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
        downloadHeaders.Add(HttpRequestHeader.Te, "trailers");

        if (!Directory.Exists(parentFolder))
        {
            Directory.CreateDirectory(parentFolder);
        }

        var downloadOpt = new DownloadConfiguration()
        {
            ChunkCount = PartyGlobals.DownloadFileParts, // file parts to download, default value is 1
            ParallelDownload = true, // download parts of file as parallel or not. Default value is
            RequestConfiguration =
            {
                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8",
                Headers = downloadHeaders, // { your custom headers }
                KeepAlive = true, // default value is false
                ProtocolVersion = HttpVersion.Version11, // default value is HTTP 1.1
                UseDefaultCredentials = false,
                // your custom user agent or your_app_name/app_version.
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:120.0) Gecko/20100101 Firefox/120.0"
            }
        };

        var downloader = new DownloadService(downloadOpt);
        try
        {
            if (File.Exists(parentFolder + "/" + fileName))
            {
                Random rng = new Random();
                downloader.DownloadFileTaskAsync(url, parentFolder + "/" + rng.Next(1, 999).ToString() + "-" + fileName).Wait();
            }
            else
            {
                downloader.DownloadFileTaskAsync(url, parentFolder + "/" + fileName).Wait();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception \"" + ex + "\" occurred while downloading " + fileName + ", skipping...");
        }
    }

    /// <summary>
    /// Downloads the specified attachment. Basically just a public wrapper.
    /// </summary>
    /// <param name="attachment"></param>
    /// <param name="parentFolder"></param>
    public void DownloadAttachment(Attachment attachment, string parentFolder)
    {
        if (attachment is { URL: not null, FileName: not null })
        {
            DownloadContent(attachment.URL, parentFolder, attachment.FileName);
        }
    }

    /// <summary>
    /// Scrapes a post for its raw media and queues referenced media for download
    /// </summary>
    /// <param name="postUrl"></param>
    /// <param name="iteration"></param>
    public Post ScrapePost(string postUrl, int iteration)
    {
        var post = new Post(postUrl, Creator);
        post.Iteration = iteration;
        post.ReverseIteration = TotalRequestedPosts - (iteration - 1);

        var postClient = new RestClient(postUrl);
        var request = new RestRequest();
        var response = postClient.GetAsync(request).Result;
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(response.Content);

        return post;
    }
}