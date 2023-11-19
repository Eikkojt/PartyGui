using HtmlAgilityPack;

namespace PartyLib.Bases;

public class Attachment
{
    /// <summary>
    /// Attachment constructor
    /// </summary>
    /// <param name="attachmentNode">HTML DOM representation of the attachment</param>
    public Attachment(HtmlNode attachmentNode)
    {
        Node = attachmentNode;
        var linkNode =
            attachmentNode.Descendants().FirstOrDefault(x =>
                x.Attributes["href"] != null); // Find first child node with a link attribute (only a nodes here)
        if (linkNode != null)
        {
            URL = linkNode.Attributes["href"].Value;
            FileName = linkNode.Attributes["download"].Value;
        }
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
}