using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PartyLib.Bases
{
    public class HttpAssetCore
    {
        /// <summary>
        /// Whether the HTTP request for the asset succeeded
        /// </summary>
        public bool SuccessfulFetch { get; protected set; } = false;

        /// <summary>
        /// Status code for the HTTP request
        /// </summary>
        public HttpStatusCode StatusCode { get; protected set; }
    }
}
