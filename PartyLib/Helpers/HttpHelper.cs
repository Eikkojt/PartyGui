using PartyLib.Bases;
using RandomUserAgent;
using RestSharp;

namespace PartyLib.Helpers;

public static class HttpHelper
{
    /// <summary>
    /// HTTP response cache (prevents overloading partysite servers)
    /// </summary>
    public static List<Tuple<string, RestResponse, DateTime>> HttpCache = new();

    /// <summary>
    /// Performs a simple GET request
    /// </summary>
    /// <param name="request">Assembled RestRequest</param>
    /// <param name="url">URL to fetch</param>
    /// <param name="defaultHeaders">
    /// Whether to use a set of default HTTP headers, set false to use your own
    /// </param>
    /// <param name="noCache">Whether to cache the HTTP response, set false to disable caching</param>
    /// <returns></returns>
    public static RestResponse? HttpGet(RestRequest request, string url, bool defaultHeaders = true, bool noCache = false)
    {
        if (HttpCache.Exists(x => x.Item1 == url) && !noCache)
        {
            return HttpCache.Find(x => x.Item1 == url).Item2;
        }
        else
        {
            string userAgent = RandomUa.RandomUserAgent;
            var client = new RestClient(url);
            if (defaultHeaders)
            {
                request.AddHeader("Accept-Encoding", "gzip, deflate, br");
                request.AddHeader("Accept-Language", "en-US,en;q=0.5");
                request.AddHeader("Sec-Fetch-Dest", "document");
                request.AddHeader("Sec-Fetch-Mode", "navigate");
                request.AddHeader("Sec-Fetch-User", "?1");
                request.AddHeader("Sec-Fetch-Site", "same-origin");
                request.AddHeader("TE", "trailers");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("User-Agent", userAgent);
            }

            try
            {
                RestResponse response = client.Get(request);
                HttpCache.Add(Tuple.Create(url, response, DateTime.Now));
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }

    /// <summary>
    /// Clears the HTTP cache. Using the nocache flag is highly recommended instead of using this.
    /// </summary>
    public static void FlushCache()
    {
        HttpCache.Clear();
    }
}