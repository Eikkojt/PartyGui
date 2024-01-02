namespace PartyLib.Config
{
    public class MegaConfig
    {
        /// <summary>
        /// Enables experimental mega.nz support. THIS REQUIRES PROXIES!
        /// </summary>
        public bool EnableMegaSupport { get; set; } = false;

        /// <summary>
        /// Path to MegaCMD install folder, if applicable. Proxifier is heavily recommended for all
        /// executables here.
        /// </summary>
        public string MegaCMDPath { get; set; } = string.Empty;
    }
}