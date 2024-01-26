using System.Diagnostics;

namespace PartyLib.Config
{
    public static class PartyConfig
    {
        /// <summary>
        /// Library's semver version
        /// </summary>
        public static string Version { get; } = "v0.7.5";

        /// <summary>
        /// Whether to automatically extract zip files. Defaults to true.
        /// </summary>
        public static bool ExtractZipFiles { get; set; } = true;

        /// <summary>
        /// (ADVANCED OPTION) Whether to strip unicode characters out of attachment names. Helps
        /// with file pathing since Windows does not like special characters.
        /// </summary>
        public static bool StripUnicodeFromAttachments { get; set; } = true;

        /// <summary>
        /// (ADVANCED OPTION) Whether to automatically override file and folder modification times
        /// for downloaded content.
        /// </summary>
        public static bool OverwriteFileModificationTimes { get; set; } = true;

        /// <summary>
        /// (ADVANCED OPTION) Whether to use sqlite support or not. This should never really be
        /// disabled unless you have your own upstream database.
        /// </summary>
        public static bool UseDatabaseSupport { get; set; } = true;

        /// <summary>
        /// PartyLib global MegaConfig instance.
        /// </summary>
        public static MegaConfig MegaOptions { get; set; } = new MegaConfig();

        /// <summary>
        /// PartyLib global translation instance.
        /// </summary>
        public static TranslationConfig TranslationConfig { get; set; } = new TranslationConfig();

        /// <summary>
        /// PartyLib global DownloadConfig instance.
        /// </summary>
        public static DownloadConfig DownloadConfig { get; set; } = new DownloadConfig();
    }
}