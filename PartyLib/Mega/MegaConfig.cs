using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyLib.Mega
{
    public static class MegaConfig
    {
        /// <summary>
        /// Enables experimental mega.nz support. THIS REQUIRES PROXIES!
        /// </summary>
        public static bool EnableMegaSupport { get; set; } = false;

        /// <summary>
        /// Path to MegaCMD install folder, if applicable. Proxifier is heavily recommended for all
        /// executables here.
        /// </summary>
        public static string MegaCMDPath { get; set; } = String.Empty;
    }
}