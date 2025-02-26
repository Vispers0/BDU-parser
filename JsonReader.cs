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
            JsonNode? rootObject = JsonObject.Parse(_jsonString);

            if (rootObject == null)
            {
                throw new NullReferenceException();
            }
            JsonNode? cwesArray = rootObject["cwes"];

            if (cwesArray == null)
            {
                throw new NullReferenceException();
            }
            JsonNode? cweObject = cwesArray[0];

            if (cweObject == null)
            {
                throw new NullReferenceException();
            }
            JsonNode? cweId = cweObject["cwe_id"];

            if (cweId == null)
            {
                throw new NullReferenceException();
            }

            return cweId.ToString();
        }
    }
}
