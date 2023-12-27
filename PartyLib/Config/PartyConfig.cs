using GTranslate.Translators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyLib.Config
{
    public static class PartyConfig
    {
        /// <summary>
        /// Library's semver version
        /// </summary>
        public static string Version { get; } = "v0.7.0";

        /// <summary>
        /// The amount of chunks the downloader function should use
        /// </summary>
        public static int DownloadFileParts { get; set; } = 1;

        /// <summary>
        /// Number of parallel connections to use for downloads
        /// </summary>
        public static int ParallelDownloadParts { get; set; } = 4;

        /// <summary>
        /// Automatically extract zip files
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