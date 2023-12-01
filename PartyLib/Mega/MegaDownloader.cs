using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartyLib.Config;

namespace PartyLib.Mega
{
    public class MegaDownloader
    {
        /// <summary>
        /// Downloads a MEGA file from a public link. Requires proxies.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parentPath"></param>
        /// <param name="password">Optional file decryption key, if not included in URL</param>
        public void ExecuteMegaGet(string url, string parentPath, string password = "")
        {
            var process = new Process();
            var processInfo = new ProcessStartInfo();
            processInfo.FileName = $"cmd.exe"; // Executed via cmd so console window shows regardless
            if (password == "")
            {
                // Passwordless download
                processInfo.Arguments = $"/C {PartyConfig.MegaOptions.MegaCMDPath + "\\MEGAclient.exe"} get --ignore-quota-warn {url} \"{parentPath}\"";
            }
            else
            {
                // Passworded download
                processInfo.Arguments = $"/C {PartyConfig.MegaOptions.MegaCMDPath + "\\MEGAclient.exe"} get  --password={password} --ignore-quota-warn {url} \"{parentPath}\"";
            }
            processInfo.WorkingDirectory = PartyConfig.MegaOptions.MegaCMDPath;
            processInfo.WindowStyle = ProcessWindowStyle.Minimized; // Try not to annoy the users
            process.StartInfo = processInfo;

            process.Start();
            process.WaitForExit();
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        public MegaDownloader()
        {
            if (PartyConfig.MegaOptions.MegaCMDPath == String.Empty)
            {
                throw new Exception("Mega downloader initialized, but MegaCMD not found! Did you set the install directory?");
            }
        }
    }
}