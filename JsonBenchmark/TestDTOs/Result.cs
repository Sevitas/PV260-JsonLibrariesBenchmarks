using System.Runtime.Serialization;

namespace JsonBenchmark.TestDTOs
{
    public class Result
    {
        public string[] category { get; set; }

        public string icon_url { get; set; }

        public string id { get; set; }

        public string url { get; set; }

        public string value { get; set; }
    }
}