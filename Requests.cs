
using System.Net.Security;

namespace bdu_parser
{
    public class Requests
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

        // Get detailed info about BDU
        public async Task<string> GetBdu(string bduId)
        {
            try
            {
                using HttpResponseMessage responce = await httpClient.GetAsync(bduId);
                responce.EnsureSuccessStatusCode();
                string responceBody = await responce.Content.ReadAsStringAsync();

                return responceBody;
            }
            catch (HttpRequestException e)
            {
                return e.Message;
            }
        }

        public async Task<string> GetCwe(string cweId)
        {
            try
            {
                using HttpResponseMessage responce = await httpClient.GetAsync(cweId);
                responce.EnsureSuccessStatusCode();
                string responceBody = await responce.Content.ReadAsStringAsync();

                return responceBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }

        public async Task<String> GetCapecDetails(string capecUrl)
        {
            try
            {
                using HttpResponseMessage responce = await httpClient.GetAsync(capecUrl);
                responce.EnsureSuccessStatusCode();
                string responceBody = await responce.Content.ReadAsStringAsync();

                return responceBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }
    }
}
