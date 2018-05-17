using System.Runtime.Serialization;
using JsonBenchmark.TestDTOs;

namespace JsonBenchmark.TestDTOs
{
    public class Root
    {
        public int total { get; set; }

        public Result[] result { get; set; }
    }
}
