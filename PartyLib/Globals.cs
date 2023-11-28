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
        /// Library's semver version
        /// </summary>
        public static string Version { get; } = "v1.0.0";

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
        /// The amount of chunks the downloader function should use
        /// </summary>
        public static int DownloadFileParts { get; set; } = 1;
    }
}