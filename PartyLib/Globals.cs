using GTranslate.Translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyLib
{
    public static class PartyGlobals
    {
        /// <summary>
        /// Translation service
        /// </summary>
        public static GoogleTranslator Translator { get; set; } = new GoogleTranslator();

        /// <summary>
        /// Whether to translate all post titles.
        /// </summary>
        public static bool TranslateTitles { get; set; } = false;

        /// <summary>
        /// Whether to translate all post descriptions (NOT RECOMMENDED - uses heavy API usage)
        /// </summary>
        public static bool TranslateDescriptions { get; set; } = false;

        /// <summary>
        /// The language to translate text into, if translation was requested
        /// </summary>
        public static string TranslationLocaleCode { get; set; } = "en";

        /// <summary>
        /// The amount of concurrent connections the downloader function should use
        /// </summary>
        public static int DownloaderFileParts { get; set; } = 5;

        /// <summary>
        /// The amount of times to attempt a download retry if the download fails
        /// </summary>
        public static int DownloaderRetries { get; set; } = 3;
    }
}