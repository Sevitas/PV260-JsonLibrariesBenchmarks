using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;
using JsonBenchmark.TestDTOs;
using Newtonsoft.Json;
using Jil;

namespace JsonBenchmark
{
    [ClrJob(isBaseline: true)]
    [RPlotExporter, RankColumn]
    [HtmlExporter]
    public class JsonDeserializersBenchmarks : JsonBenchmarkBase
    {
        [Benchmark]
        public Root NewtonsoftJson_Deserialize()
        {
            return JsonConvert.DeserializeObject<Root>(JsonSampleString);
        }

        [Benchmark]
        public Root NewtonsoftJson_NotSpeedOptimalized_Deserialize()
        {
            return JsonConvert.DeserializeObject<Root>(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "TestFiles", "chucknorris.json")));
        }

        [Benchmark]
        public Root NewtonsoftJson_SpeedOptimilized_Deserialize()
        {

            using (Stream s = File.OpenRead("TestFiles/chucknorris.json"))
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                return new JsonSerializer().Deserialize<Root>(reader);
            }
        }

        [Benchmark]
        public Root DataContractJsonSerializer_Deserialize()
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(JsonSampleString)))
            {
                return new DataContractJsonSerializer(typeof(Root)).ReadObject(stream) as Root;
            }
        }

        [Benchmark]
        public Root JIL_Deserialize()
        {
            return JSON.Deserialize<Root>(JsonSampleString);
        }

        [Benchmark]
        public Friends NewtonsoftJson_Friend_Deserialize()
        {
            return JsonConvert.DeserializeObject<Friends>(JsonSampleStringFriend);
        }

        [Benchmark]
        public Root JavaScriptSerializer_Deserialize()
        {
            return new JavaScriptSerializer().Deserialize<Root>(JsonSampleString);
        }
    }
}
