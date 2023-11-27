using System.Text.RegularExpressions;
using HtmlAgilityPack;
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
        request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:120.0) Gecko/20100101 Firefox/120.0");

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
        if (PartyGlobals.TranslateTitles)
        {
            try
            {
                string translatedTitle = PartyGlobals.Translator.TranslateAsync(postTitle, PartyGlobals.TranslationLocaleCode).Result.Translation;
                Title = Strings.SanitizeText(translatedTitle);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred during post title translation: {ex.Message}. Disabling translations for all future jobs. To re-enable, set the global variable back to true.");
                PartyGlobals.TranslateTitles = false;
                Title = Strings.SanitizeText(postTitle).Trim();
            }
        }
        else
        {
            Title = Strings.SanitizeText(postTitle).Trim();
        }

        // Text for posts
        var contentNode = responseDocument.DocumentNode.Descendants().FirstOrDefault(x => x.HasClass("post__content") && x.Name == "div");
        if (contentNode != null)
        {
            var scrDesc = contentNode.InnerText;
            if (scrDesc.StartsWith("\n")) scrDesc = scrDesc.Remove(0);
            foreach (var child in contentNode.ChildNodes) scrDesc = scrDesc + child.InnerText + "\n";
            if (PartyGlobals.TranslateDescriptions)
            {
                try
                {
                    string translatedDescription = PartyGlobals.Translator.TranslateAsync(scrDesc, PartyGlobals.TranslationLocaleCode).Result.Translation;
                    Description = Strings.SanitizeText(translatedDescription);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred during post description translation: {ex.Message}. Disabling translations for all future jobs. To re-enable, set the global variable back to true.");
                    PartyGlobals.TranslateDescriptions = false;
                    Description = scrDesc.Trim();
                }
            }
            else
            {
                Description = scrDesc.Trim();
            }
        }

        // Image posts (usually)
        var filesNode = responseDocument.DocumentNode.Descendants().FirstOrDefault(x => x.HasClass("post__files") && x.Name == "div");
        if (filesNode != null)
            foreach (var post in filesNode.ChildNodes)
            {
                if (post.NodeType != HtmlNodeType.Element) // We don't want text or comments
                {
                    continue;
                }
                var attachment = new Attachment(post);
                Images?.Add(attachment);
            }

        // Attachment posts
        var attachmentNode = responseDocument.DocumentNode.Descendants().FirstOrDefault(x => x.HasClass("post__attachments") && x.Name == "ul");
        if (attachmentNode != null)
            foreach (var post in attachmentNode.ChildNodes)
            {
                if (post.NodeType != HtmlNodeType.Element) // We don't want text or comments
                {
                    continue;
                }
                var attachment = new Attachment(post);
                Attachments?.Add(attachment);
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
    public List<Attachment>? Images { get; } = new();

    /// <summary>
    /// A list of attachments attached to the post
    /// </summary>
    public List<Attachment>? Attachments { get; } = new();

    /// <summary>
    /// The post's associated creator
    /// </summary>
    public Creator Creator { get; private set; }
}