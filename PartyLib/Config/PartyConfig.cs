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
        /// A list of errors discovered by the downloader
        /// </summary>
        public static List<Tuple<Exception, string>> DownloaderErrors = new List<Tuple<Exception, string>>();

        /// <summary>
        /// Library's semver version
        /// </summary>
        public static string Version { get; } = "v0.6.0";

        /// <summary>
        /// The amount of chunks the downloader function should use
        /// </summary>
        public static int DownloadFileParts { get; set; } = 1;

        /// <summary>
        /// Number of parallel connections to use for downloads
        /// </summary>
        public static int ParallelDownloadParts { get; set; } = 4;

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