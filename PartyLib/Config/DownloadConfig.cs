using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyLib.Config
{
    public class DownloadConfig
    {
        /// <summary>
        /// The amount of chunks the downloader function should use. Defaults to 1.
        /// </summary>
        public int DownloadFileParts { get; set; } = 1;

        /// <summary>
        /// Number of parallel connections to use for downloads. Defaults to 4.
        /// </summary>
        public int ParallelDownloadParts { get; set; } = 8;

        /// <summary>
        /// Number of times to retry a failed download.
        /// </summary>
        public int DownloadFailRetries { get; set; } = 5;
    }
}