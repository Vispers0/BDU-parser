using System.Text.Json.Nodes;

namespace bdu_parser
{
    public class JsonReader
    {

        private readonly string _jsonString;

        public JsonReader(string jsonString)
        {
            _jsonString = jsonString;
        }

        public string Deserealize()
        {
            var rootObject = JsonObject.Parse(_jsonString);
            var cwesArray = rootObject["cwes"];
            var cweObject = cwesArray[0];
            var cweId = cweObject["cwe_id"];

            return cweId.ToString();
        }
    }
}
