using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyLib.Mega
{
    public class MegaDownloader
    {
        /// <summary>
        /// Downloads a MEGA file from a public link. Requires proxies.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parentPath"></param>
        /// <param name="password"></param>
        public void ExecuteMegaGet(string url, string parentPath, string password = "")
        {
            var process = new Process();
            var processInfo = new ProcessStartInfo();
            processInfo.FileName = $"cmd.exe";
            if (password == "")
            {
                processInfo.Arguments = $"/C {PartyConfig.MegaCMDPath + "\\MEGAclient.exe"} get --ignore-quota-warn {url} \"{parentPath}\"";
            }
            else
            {
                processInfo.Arguments = $"/C {PartyConfig.MegaCMDPath + "\\MEGAclient.exe"} get  --password={password} --ignore-quota-warn {url} \"{parentPath}\"";
            }
            processInfo.WorkingDirectory = PartyConfig.MegaCMDPath;
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo = processInfo;

            process.Start();
            process.WaitForExit();
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        public MegaDownloader()
        {
            if (PartyConfig.MegaCMDPath == String.Empty)
            {
                throw new Exception("Mega downloader initialized, but MegaCMD not found! Did you set the install directory?");
            }
        }
    }
}