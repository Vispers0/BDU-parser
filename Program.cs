namespace bdu_parser
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            const string path = "C:\\Sharaga\\ZIvKS\\LR2\\vulnerabilities.html";

            HtmlReader htmlReader = new HtmlReader(path);

            List<string> bdus = htmlReader.ReadFromFile();

            Requests requests = new Requests("https://bdu.fstec.ru/vul/");
            string responce  = await requests.GetBdu("2025-01829");

            htmlReader.ReadFromString(responce);
        }
    }
}
