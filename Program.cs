namespace bdu_parser
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            const string path = "C:\\Sharaga\\ZIvKS\\LR2\\vulnerabilities.html";

            HtmlReader htmlReader = new HtmlReader(path);
            JsonReader jsonReader;

            // Read scanOval .html report
            List<string> bdus = htmlReader.ReadFromFile();

            //Get JSON data about BDU
            Requests requests = new Requests("https://bdu.fstec.ru/vul/");
            string bduResponce  = await requests.GetBdu("2025-01829");
            string jsonResponce = htmlReader.ReadScriptFromString(bduResponce);

            //Parse JSON for CWE id
            jsonReader = new JsonReader(jsonResponce);
            string cweId = jsonReader.Deserealize() + ".html";

            //Get CAPECS links
            requests = new Requests("https://cwe.mitre.org/data/definitions/");
            string cweResponce = await requests.GetCwe(cweId);

            List<string> capecsLinks = htmlReader.ReadFromString(cweResponce);
        }
    }
}
