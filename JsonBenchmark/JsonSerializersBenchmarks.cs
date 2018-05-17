using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;
using Jil;
using JsonBenchmark.TestDTOs;
using Newtonsoft.Json;

namespace JsonBenchmark
{
    [ClrJob(isBaseline: true)]
    [RPlotExporter, RankColumn]
    [HtmlExporter]
    public class JsonSerializersBenchmarks : JsonBenchmarkBase
    {
        private readonly Root serializationObjectsRoot;
        private readonly Friends serializationObjectsFriends;
        private const string TestFilesFolder = "TestFiles";

        public JsonSerializersBenchmarks()
        {
            this.serializationObjectsRoot = JsonConvert.DeserializeObject<Root>(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, TestFilesFolder, "chucknorris.json")));
            this.serializationObjectsFriends = JsonConvert.DeserializeObject<Friends>(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, TestFilesFolder, "friend.json")));
        }

        [Benchmark]
        public String NewtonsoftJson_Serialize()
        {
            return JsonConvert.SerializeObject(serializationObjectsRoot);
        }

        [Benchmark]
        public String NewtosoftJson_SpeedOptimalized_Serialize()
        {
            return serializeRoot();
        }

        [Benchmark]
        public String NewtonsoftJson_Friend_Serialize()
        {
            return JsonConvert.SerializeObject(serializationObjectsFriends);
        }

        [Benchmark]
        public String JavaScriptSerializer_Serialize()
        {
            return new JavaScriptSerializer().Serialize(serializationObjectsRoot);
        }

        [Benchmark]
        public String Jil_Serialize()
        {
            using (TextWriter writer = new StringWriter())
            {
                JSON.Serialize(serializationObjectsRoot, writer);
                return writer.ToString();
            }
        }

        [Benchmark]
        public String DataContractJsonSerializer_Serialize()
        {
            using (var ms = new MemoryStream())
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Root));
                ser.WriteObject(ms, serializationObjectsRoot);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        private String serializeRoot()
        {
            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            writer.WriteStartObject();

            writer.WritePropertyName("total");
            writer.WriteValue(serializationObjectsRoot.total);

            writer.WritePropertyName("result");
            writer.WriteStartArray();
            foreach (Result result in serializationObjectsRoot.result)
            {
                writer.WriteStartObject();

                writer.WritePropertyName("category");
                if (result.category != null)
                {
                    writer.WriteStartArray();
                    foreach (string category in result.category)
                    {
                        writer.WriteValue(category);
                    }
                    writer.WriteEndArray();
                }
                else
                    writer.WriteNull();

                writer.WritePropertyName("icon_url");
                writer.WriteValue(result.icon_url);

                writer.WritePropertyName("id");
                writer.WriteValue(result.id);

                writer.WritePropertyName("url");
                writer.WriteValue(result.url);

                writer.WritePropertyName("value");
                writer.WriteValue(result.value);

                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            writer.WriteEndObject();

            return sw.ToString();
        }
    }
}
