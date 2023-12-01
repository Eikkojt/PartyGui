using System.Text.RegularExpressions;
using HtmlAgilityPack;
using PartyLib.Config;
using RandomUserAgent;
using RestSharp;

namespace PartyLib.Bases;

public class Post
{
    /// <summary>
    /// Post constructor. Most fields are filled here.
    /// </summary>
    /// <param name="url">The post's partysite URL</param>
    /// <param name="creator">The creator of the post</param>
    public Post(string url, Creator creator)
    {
        // HTTP Management
        string userAgent = RandomUa.RandomUserAgent;
        var client = new RestClient(url);
        var request = new RestRequest();
        request.AddHeader("Accept-Encoding", "gzip, deflate, br");
        request.AddHeader("Accept-Language", "en-US,en;q=0.5");
        request.AddHeader("Sec-Fetch-Dest", "document");
        request.AddHeader("Sec-Fetch-Mode", "navigate");
        request.AddHeader("Sec-Fetch-User", "?1");
        request.AddHeader("Sec-Fetch-Site", "same-origin");
        request.AddHeader("TE", "trailers");
        request.AddHeader("Referer", creator.URL);
        //request.AddHeader("Cookie", "__ddg1_=T5K2HugyIgOY9MsrbsfC; thumbSize=180");
        request.AddHeader("Connection", "keep-alive");
        request.AddHeader("User-Agent", userAgent);

        // Perform HTTP request
        var response = client.GetAsync(request).Result;
        var responseDocument = new HtmlDocument();
        responseDocument.LoadHtml(response.Content);

        // Fetch post ID
        var postIDFinder = new Regex("/post/(.*)");
        var postIDMatch = postIDFinder.Match(url);
        var postId = postIDMatch.Groups[1].Value;
        ID = int.Parse(postId);

        // Fetch post title
        var titleParent = responseDocument.DocumentNode.Descendants().FirstOrDefault(x => x.HasClass("post__title") && x.Name == "h1");
        var titleSpan = titleParent.ChildNodes.FirstOrDefault(x => x.Name == "span");
        var postTitle = titleSpan.InnerText;

        if (postTitle == "Untitled")
        {
            postTitle = postTitle + " (Post ID∶ " + ID + ")";
        }

        // Translate post title if applicable
        if (PartyConfig.TranslateTitles)
        {
            try
            {
                string translatedTitle = PartyConfig.Translator.TranslateAsync(postTitle, PartyConfig.TranslationLocaleCode).Result.Translation;
                Title = translatedTitle;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred during post title translation: {ex.Message}. Disabling translations for all future jobs. To re-enable, set the global variable back to true.");
                PartyConfig.TranslateTitles = false;
                Title = postTitle.Trim();
            }
        }
        else
        {
            Title = postTitle.Trim();
        }

        // Text for posts
        var contentNode = responseDocument.DocumentNode.Descendants().FirstOrDefault(x => x.HasClass("post__content") && x.Name == "div");
        if (contentNode != null)
        {
            // MEGA links
            if (PartyConfig.MegaOptions.EnableMegaSupport)
            {
                List<HtmlNode> megaLinks = contentNode.Descendants().Where(x => x.Attributes["href"] != null && x.Attributes["href"].Value.Contains("https://mega.nz")).ToList();
                foreach (var megaLink in megaLinks)
                {
                    this.MegaUrls.Add(megaLink.Attributes["href"].Value);
                }
            }

            // Content stuff
            var scrDesc = contentNode.InnerText;
            if (scrDesc.StartsWith("\n"))
            {
                scrDesc = scrDesc.Remove(0);
            }
            foreach (var child in contentNode.ChildNodes) scrDesc = scrDesc + child.InnerText + "\n";
            if (PartyConfig.TranslateDescriptions)
            {
                try
                {
                    string translatedDescription = PartyConfig.Translator.TranslateAsync(scrDesc, PartyConfig.TranslationLocaleCode).Result.Translation;
                    Description = translatedDescription.Trim();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred during post description translation: {ex.Message}. Disabling translations for all future jobs. To re-enable, set the global variable back to true.");
                    PartyConfig.TranslateDescriptions = false;
                    Description = scrDesc.Trim();
                }
            }
            else
            {
                Description = scrDesc.Trim();
            }
        }

        // Files
        var filesNode = responseDocument.DocumentNode.Descendants().FirstOrDefault(x => x.HasClass("post__files") && x.Name == "div");
        if (filesNode != null)
        {
            List<HtmlNode> files = filesNode.Descendants().Where(x => x.Attributes["href"] != null && x.Attributes["download"] != null).ToList();
            foreach (var file in files)
            {
                var filey = new Attachment(file);
                Files?.Add(filey);
            }
        }

        // Attachment posts
        var attachmentNode = responseDocument.DocumentNode.Descendants().FirstOrDefault(x => x.HasClass("post__attachments") && x.Name == "ul");
        if (attachmentNode != null)
        {
            List<HtmlNode> rawAttachments = attachmentNode.Descendants().Where(x => x.Attributes["href"] != null && x.Attributes["download"] != null).ToList();
            foreach (var attachment in rawAttachments)
            {
                var attachy = new Attachment(attachment);
                Attachments?.Add(attachy);
            }
        }

        Creator = creator;
    }

    /// <summary>
    /// The post's human-friendly title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The post's human-friendly description
    /// </summary>
    public string? Description { get; private set; } = string.Empty;

    /// <summary>
    /// Post's URL
    /// </summary>
    public string? URL { get; set; } = string.Empty;

    /// <summary>
    /// The post's internal ID
    /// </summary>
    public int ID { get; }

    /// <summary>
    /// Post iteration relative to the total posts number
    /// </summary>
    public int Iteration { get; set; }

    /// <summary>
    /// Reversed post iteration
    /// </summary>
    public int ReverseIteration { get; set; }

    /// <summary>
    /// A list of images attached to the post
    /// </summary>
    public List<Attachment>? Files { get; } = new();

    /// <summary>
    /// A list of attachments attached to the post
    /// </summary>
    public List<Attachment>? Attachments { get; } = new();

    /// <summary>
    /// The post's associated creator
    /// </summary>
    public Creator Creator { get; private set; }

    /// <summary>
    /// A list of any found MEGA urls
    /// </summary>
    public List<string> MegaUrls { get; private set; } = new List<string>();
}