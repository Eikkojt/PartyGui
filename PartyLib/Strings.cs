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
}