namespace PartyLib.Config
{
    public static class PartyConfig
    {
        /// <summary>
        /// Library's semver version
        /// </summary>
        public static string Version { get; } = "v0.7.1";

        /// <summary>
        /// The amount of chunks the downloader function should use. Defaults to 1.
        /// </summary>
        public static int DownloadFileParts { get; set; } = 1;

        /// <summary>
        /// Number of parallel connections to use for downloads. Defaults to 4.
        /// </summary>
        public static int ParallelDownloadParts { get; set; } = 4;

        /// <summary>
        /// Whether to automatically extract zip files. Defaults to true.
        /// </summary>
        public static bool ExtractZipFiles { get; set; } = true;

        /// <summary>
        /// PartyLib global MegaConfig instance.
        /// </summary>
        public static MegaConfig MegaOptions { get; set; } = new MegaConfig();

        /// <summary>
        /// PartyLib global translation instance.
        /// </summary>
        public static TranslationConfig TranslationConfig { get; set; } = new TranslationConfig();
    }
}