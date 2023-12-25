using DiscordRPC.Logging;

namespace KemonoScraperSharp_GUI.Helpers;

public static class DiscordHelper
{
    /// <summary>
    /// Discord rich presence client ID
    /// </summary>
    public static readonly string ClientID = "1188247032754880702";

    /// <summary>
    /// Discord rich presence logging schema
    /// </summary>
    public static readonly ILogger ClientLogger = new ConsoleLogger() { Level = LogLevel.Warning };
}