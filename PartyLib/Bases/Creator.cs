using HtmlAgilityPack;
using PartyLib.Helpers;
using RestSharp;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;

namespace PartyLib.Bases;

public class Creator : HttpAssetCore
{
    /// <summary>
    /// List of supported services
    /// </summary>
    private List<string> servicesList = new List<string>
    {
        "fanbox",
        "patreon",
        "fantia",
        "subscribestar",
        "gumroad",
        "boosty",
        "onlyfans",
        "fansly"
    };

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="url">Creator's party site URL</param>
    public Creator(string url)
    {
        URL = url; // Simple variable setting

        // HTTP init
        var response = HttpHelper.HttpGet(new RestRequest(), url);
        if (response.IsSuccessful == false)
        {
            // Response was recieved but failed
            this.SuccessfulFetch = false;
            this.StatusCode = response.StatusCode;
        }
        else if (response == null)
        {
            // No response was recieved and something went very wrong
            this.SuccessfulFetch = false;
            this.StatusCode = HttpStatusCode.MethodNotAllowed;
        }
        else
        {
            // Successful HTTP fetch
            this.SuccessfulFetch = true;
            this.StatusCode = response.StatusCode;

            // Load HTML
            var responseDocument = new HtmlDocument();
            responseDocument.LoadHtml(response.Content);
            LandingPage = responseDocument;

            // Populate name variable
            var creatorNameNode = responseDocument.DocumentNode.SelectNodes("//span[@itemprop]").FirstOrDefault();
            Name = creatorNameNode?.InnerText;

            // Identify service
            if (servicesList.Any(url.Contains))
            {
                Service = servicesList.Find(url.Contains);
            }
            else
            {
                // Unsupported service
                Service = null;
            }

            // Fetch domain URL
            var reg = new Regex("https://[A-Za-z0-9]+\\.su");
            var regMatch = reg.Match(url);
            if (regMatch.Success)
            {
                PartyDomain = regMatch.Value;
            }
            else
            {
                PartyDomain = null;
            }
        }
    }

    /// <summary>
    /// Human-readable name of the creator
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The creator's party site URL
    /// </summary>
    public string URL { get; private set; }

    /// <summary>
    /// The archival service used by the creator
    /// </summary>
    public string? Service { get; private set; }

    /// <summary>
    /// Whether the creator's domain is from coomer or kemono
    /// </summary>
    public string? PartyDomain { get; private set; }

    /// <summary>
    /// Creator's landing page source code
    /// </summary>
    public HtmlDocument LandingPage { get; }

    /// <summary>
    /// Cache variable for GetProfilePicture()
    /// </summary>
    private Image? ProfilePicture = null;

    /// <summary>
    /// Cache variable for GetProfileBanner()
    /// </summary>
    private Image? ProfileBanner = null;

    /// <summary>
    /// Fetches the total number of posts a creator has on their service
    /// </summary>
    /// <returns></returns>
    public int GetTotalPosts()
    {
        // Posts integer is ambiguous and fetches all posts
        var totalPostsNode =
            LandingPage.DocumentNode.SelectSingleNode(
                "/html/body/div[2]/main/section/div[1]/small"); // Text element that displays under the creator's banner at the top
        if (totalPostsNode != null)
            return int.Parse(totalPostsNode.InnerText.Replace("Showing 1 - 50 of ", ""));
        return -1;
    }

    /// <summary>
    /// Fetches a creator's profile picture
    /// </summary>
    /// <returns></returns>
    public Image? GetProfilePicture()
    {
        if (ProfilePicture == null)
        {
            // Fetch the actual image URL
            var imageNode = LandingPage.DocumentNode.Descendants().FirstOrDefault(x => x.HasClass("fancy-image__image") && x.Name == "img" && x.Attributes["src"].Value.Contains("icons"));

            // HTTP Weird Stuff
            var imgClient = new RestClient("https:" + imageNode.Attributes["src"].Value);
            var imgRequest = new RestRequest();
            var profilePicData = imgClient.DownloadData(imgRequest); // RAM usage go brrrrrr
            if (profilePicData != null)
            {
                using var ms = new MemoryStream(profilePicData);
                ProfilePicture = Image.FromStream(ms);
                return Image.FromStream(ms);
            }
            else
            {
                return null;
            }
        }
        else
        {
            return ProfilePicture;
        }
    }

    /// <summary>
    /// Fetches a creator's profile picture URL
    /// </summary>
    /// <returns></returns>
    public string? GetProfilePictureURL()
    {
        // Fetch the actual image URL
        var imageNode = LandingPage.DocumentNode.Descendants().FirstOrDefault(x => x.HasClass("fancy-image__image") && x.Name == "img" && x.Attributes["src"].Value.Contains("icons"));

        return "https:" + imageNode.Attributes["src"].Value;
    }

    /// <summary>
    /// Fetches a creator's profile banner
    /// </summary>
    /// <returns></returns>
    public Image? GetProfileBanner()
    {
        if (ProfileBanner == null)
        {
            // Fetch the actual image URL
            var imageNode = LandingPage.DocumentNode.Descendants().FirstOrDefault(x => x.HasClass("fancy-image__image") && x.Name == "img" && x.Attributes["src"].Value.Contains("banners"));

            // HTTP Weird Stuff
            var imgClient = new RestClient("https:" + imageNode.Attributes["src"].Value);
            var imgRequest = new RestRequest();
            var profilePicData = imgClient.DownloadData(imgRequest); // RAM usage go brrrrrr
            if (profilePicData != null)
            {
                using var ms = new MemoryStream(profilePicData);
                ProfilePicture = Image.FromStream(ms);
                return Image.FromStream(ms);
            }
            else
            {
                return null;
            }
        }
        else
        {
            return ProfileBanner;
        }
    }

    /// <summary>
    /// Fetches a creator's profile banner URL
    /// </summary>
    /// <returns></returns>
    public string? GetProfileBannerURL()
    {
        // Fetch the actual image URL
        var imageNode = LandingPage.DocumentNode.Descendants().FirstOrDefault(x => x.HasClass("fancy-image__image") && x.Name == "img" && x.Attributes["src"].Value.Contains("banners"));

        return "https:" + imageNode.Attributes["src"].Value;
    }
}