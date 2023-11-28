using System.Text.RegularExpressions;

namespace PartyLib;

public static class Strings
{
    /// <summary>
    /// Sanitizes text for usage in folder names. Has very specialized usage.
    /// </summary>
    /// <param name="text"></param>
    /// <returns>The sanitized string</returns>
    public static string SanitizeText(string text)
    {
        return text.Replace("&#39;", "'").Replace("&amp;", "&").Replace(".", "").Replace(":", "∶").Replace("?", "？")
            .Replace("’", "'").Replace("\n", "").Replace("\"", "“").Replace("&#34", "“").Replace("*", "").Replace("<", "").Replace(">", "")
            .Replace("/", "⧸").Replace("|", "⏐").Replace("\\", "⧸").Replace(((char)0).ToString(), "").Replace("\t", "").Replace("\r", "").Trim();
    }

    /// <summary>
    /// Sanitizes a file name for use in downloading
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public static string SanitizeFile(string filename)
    {
        return filename.Replace("\t", "").Replace("\r", "").Replace("\n", "").Replace("|", "").Replace("/", "").Replace("\\", "").Replace("~", "").Replace(" ", "_").Trim();
    }
}