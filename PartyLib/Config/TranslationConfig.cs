using GTranslate.Translators;

namespace PartyLib.Config
{
    public class TranslationConfig
    {
        /// <summary>
        /// The language to translate text into, if translation was requested
        /// </summary>
        public string TranslationLocaleCode { get; set; } = "en";

        /// <summary>
        /// Whether to translate all post titles.
        /// </summary>
        public bool TranslateTitles { get; set; } = false;

        /// <summary>
        /// Whether to translate all post descriptions (NOT RECOMMENDED - uses heavy API usage)
        /// </summary>
        public bool TranslateDescriptions { get; set; } = false;

        /// <summary>
        /// Whether to translate attachment names (NOT RECOMMENDED - uses heavy API usage)
        /// </summary>
        public bool TranslateAttachments { get; set; } = false;

        /// <summary>
        /// Translation service
        /// </summary>
        public GoogleTranslator Translator { get; set; } = new GoogleTranslator();
    }
}