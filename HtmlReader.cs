using HtmlAgilityPack;

namespace bdu_parser
{
    public class HtmlReader
    {
        public string Path { get; set; }
        JsonReader reader;

        public HtmlReader(string path)
        {
            Path = path;
        }

        public List<string> ReadFromFile()
        {
            List<string> outputBdus = new List<string>();

            var doc = new HtmlDocument();
            doc.Load(Path);

            var bduIdNodes = doc.DocumentNode.SelectNodes("//td[contains(@class, 'bdu')]");

            foreach (var item in bduIdNodes)
            {
                outputBdus.Add(item.InnerText);
            }

            return outputBdus;
        }

        public List<string> ReadFromString(string input)
        {
            List<string> output = new List<string>();

            var doc = new HtmlDocument();
            doc.LoadHtml(input);

            var capecIdNodes = doc.DocumentNode.SelectNodes("//a[contains(@href, 'http://capec.mitre.org/data/definitions/')]");

            foreach(var item in capecIdNodes)
            {
                output.Add(item.Attributes["href"].Value);
            }

            return output;
        }

        public string ReadScriptFromString(string input)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(input);

            var cweNodes = doc.DocumentNode.SelectNodes("//script[contains(., 'const v_model = reactive')]");

            try
            {
                string cweJson = cweNodes[0].InnerText.Split("reactive")[2];

                cweJson = cweJson.Remove(cweJson.LastIndexOf("});"));
                cweJson = cweJson.Remove(0, 1);
                cweJson = cweJson.Insert(cweJson.Length, "}");

                return cweJson;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured while converting to JSON: {e.Message}");
                Console.WriteLine($"StackTrace: {e.StackTrace}");
                return "";
            }
        }
    }
}
