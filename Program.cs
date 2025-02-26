using System.ComponentModel;

namespace bdu_parser
{
    // TODO: Rewrite the program to parse .xlsx files instead of bdu.fstec.ru

    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Path to the ScanOVAL html report
            const string path = "C:\\Sharaga\\ZIvKS\\LR2\\vulnerabilities.html";

            HtmlReader htmlReader = new HtmlReader(path);
            JsonReader jsonReader;

            // Read ScanOVAL .html report
            List<string> bdus = htmlReader.ReadFromFile();
            List<string> bduResponces = new List<string>();

            // Get BDUs ids only
            for (int i = 0; i< bdus.Count; i++)
            {
                bdus[i] = bdus[i].Substring(4, 10);
            }

            //Get JSON data about BDU
            Console.WriteLine("Parsing BDU data...");

            Requests requests = new Requests("https://bdu.fstec.ru/vul/");

            for (int i = 1; i < bdus.Count; i++)
            {
                Console.WriteLine(i);
                //bduResponces.Add(await requests.GetBdu(bdus[i]));
            }

            //string bduResponce  = await requests.GetBdu("2025-01829");
            //string jsonResponce = htmlReader.ReadScriptFromString(bduResponce);

            //Parse JSON for CWE id
            //jsonReader = new JsonReader(jsonResponce);
            //string cweId = "";
            //try
            //{
            //    cweId = jsonReader.Deserealize() + ".html";
            //}
            //catch (NullReferenceException e)
            //{
            //    Console.WriteLine("Error while parsing JSON: " + e.Message);
            //    return;
            //}

            ////Get CAPECS links
            //requests = new Requests("https://cwe.mitre.org/data/definitions/");
            //string cweResponce = await requests.GetCwe(cweId);

            //List<string> capecsLinks = htmlReader.ReadFromString(cweResponce);
            //foreach (string link in capecsLinks)
            //{
            //    Console.WriteLine(link);
            //}
        }
    }
}
