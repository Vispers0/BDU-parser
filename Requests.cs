
using System.Net.Security;

namespace bdu_parser
{
    class Requests
    {
        private HttpClient httpClient;

        public Requests(string url)
        {
            //Bypass certificate check
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            httpClient = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri(url)
            };
        }

        public async Task<string> GetBdu(string bduId)
        {
            try
            {
                using HttpResponseMessage responce = await httpClient.GetAsync(httpClient.BaseAddress + bduId);
                responce.EnsureSuccessStatusCode();
                string responceBody = await responce.Content.ReadAsStringAsync();

                return responceBody;
                
            }
            catch (HttpRequestException e)
            {
                return e.Message;
            }
        }
    }
}
