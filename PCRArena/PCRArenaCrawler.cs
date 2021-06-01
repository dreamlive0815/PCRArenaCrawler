using System;

using SimpleHTTPClient;

namespace PCRArena
{
    public class PCRArenaCrawler : IDisposable
    {


        private Client httpClient;

        public PCRArenaCrawler()
        {
            httpClient = new Client();
        }

        public void Dispose()
        {
            httpClient?.Dispose();
        }
    }
}