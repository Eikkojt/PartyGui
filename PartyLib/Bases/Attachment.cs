using HtmlAgilityPack;

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
        FileName = System.Net.WebUtility.HtmlDecode(attachmentNode.Attributes["download"].Value);
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