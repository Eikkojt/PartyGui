using HtmlAgilityPack;
using PartyLib.Config;
using PartyLib.Helpers;
using System.Web;

namespace PartyLib.Bases;

public class Attachment
{
    /// <summary>
    /// Attachment constructor
    /// </summary>
    /// <param name="attachmentNode">HTML DOM representation of the attachment</param>
    public Attachment(HtmlNode attachmentNode, Post post)
    {
        Node = attachmentNode;
        URL = attachmentNode.Attributes["href"].Value;
        if (PartyConfig.StripUnicodeFromAttachments)
        {
            FileName = StringHelper.SanitizeFile(HttpUtility.UrlDecode(attachmentNode.Attributes["download"].Value));
        }
        else
        {
            FileName = HttpUtility.UrlDecode(attachmentNode.Attributes["download"].Value);
        }
        Post = post;
    }

    /// <summary>
    /// The attachment's raw URL
    /// </summary>
    public string? URL { get; private set; }

    /// <summary>
    /// The attachment's filename, as assigned by the partysites
    /// </summary>
    public string? FileName { get; private set; }

    /// <summary>
    /// The attachment's HTML DOM node
    /// </summary>
    public HtmlNode Node { get; private set; }

    /// <summary>
    /// The attachment's parent post
    /// </summary>
    public Post Post { get; private set; }
}