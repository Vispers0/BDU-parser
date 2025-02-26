using HtmlAgilityPack;

namespace bdu_parser
{
    public class HtmlReader
    {
        public string Path { get; set; }

        public HtmlReader(string path)
        {
            Path = path;
        }

        public List<string> ReadFromFile()
        {
            List<string> outputBdus = new List<string>();

            // Load the .html document
            var doc = new HtmlDocument();
            doc.Load(Path);

            // Get all <td> nodes which satisfy the condition
            var bduIdNodes = doc.DocumentNode.SelectNodes("//td[contains(@class, 'bdu')]");

            // Add contents of the node to the list
            foreach (var item in bduIdNodes)
            {
                outputBdus.Add(item.InnerText);
            }

            return outputBdus;
        }

        public List<string> ReadFromString(string input)
        {
            List<string> output = new List<string>();

            // Load the .html document
            var doc = new HtmlDocument();
            doc.LoadHtml(input);

            // Get all <a> nodes which satisfy the condition
            var capecIdNodes = doc.DocumentNode.SelectNodes("//a[contains(@href, 'http://capec.mitre.org/data/definitions/')]");

            // Add values of the href attributes to the list
            foreach(var item in capecIdNodes)
            {
                output.Add(item.Attributes["href"].Value);
            }

            return output;
        }

        // Ubelievable kostyl
        public string ReadScriptFromString(string input)
        {
            // Load the .html document
            var doc = new HtmlDocument();
            doc.LoadHtml(input);

            // Get all <script> nodes which satisfy the condition
            var cweNodes = doc.DocumentNode.SelectNodes("//script[contains(., 'const v_model = reactive')]");

            try
            {
                // Get JSON part of the <script> node
                string cweJson = cweNodes[0].InnerText.Split("reactive")[2];

                // Make JSON part valid
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

        // Read CAPEC's description and likelihood of attack
        public void ReadCapecDetails(string input)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(input);

            var capecDescriptionNode = doc.DocumentNode.SelectNodes("//div[contains(@class, 'indent')]");
            Console.WriteLine(capecDescriptionNode[0].InnerText);

            var likelihoodOfAttackNode = doc.DocumentNode.SelectNodes("//div[contains(@id, 'Likelihood Of Attack')]");

            if (likelihoodOfAttackNode == null)
            {
                Console.WriteLine("Likelihood of attack not found");
            }
            else
            {
                Console.WriteLine(likelihoodOfAttackNode[0].InnerText);
            }
        }
    }
}
