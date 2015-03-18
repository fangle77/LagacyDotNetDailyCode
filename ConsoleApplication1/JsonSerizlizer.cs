using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication1
{
    class JsonSerizlizer
    {
        public static string SerializeWithMs(object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }

        public static string SerializeWithNewton(object obj)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new MicrosoftDateTimeConverter());
            StringBuilder sb = new StringBuilder();
            serializer.Serialize(new StringWriter(sb), obj);
            return sb.ToString();
        }

        public static void Deserializ()
        {
            string input = "{\"OrderId\":2901412,\"TradeStatus\":\"TRADE_FISNISHED\",\"InnerObj\":{\"abc\":1,\"def\":false}}";
            //input = "";
            //input = "OrderId=1123123&tradeStatus =TRADE_STATUS";
            var obj = JsonConvert.DeserializeObject<dynamic>(input);
            //Console.WriteLine(obj["OrderId"]);
            //Console.WriteLine(obj["TradeStatus"]);

            var jobject = obj as JObject;
           foreach(var x in jobject.Properties())
           {
               Console.WriteLine(x.Name);
               string xv = x.Value.ToString();
               Console.WriteLine(xv);
               Console.WriteLine(x.Value.ToString());
           }

            Console.WriteLine(obj is IDictionary<string, object>);
            Console.WriteLine(obj is ExpandoObject);

            Console.WriteLine(obj);
        }
    }

    class JsonSerializerTest
    {
        public static void SerializeTest(object obj)
        {
            JsonSerizlizer.Deserializ();

            return;

            string ms = JsonSerizlizer.SerializeWithMs(obj);
            string nt = JsonSerizlizer.SerializeWithNewton(obj);

            string split = "\r\n\r\n===============" + DateTime.Now.ToString() + "===========================\r\n\r\n";
            File.AppendAllText("ms.txt", split);
            File.AppendAllText("ms.txt", ms);

            File.AppendAllText("newton.txt", split);
            File.AppendAllText("newton.txt", nt);
        }
    }

    public class MicrosoftDateTimeConverter : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            if (!(value is DateTime))
            {
                throw new JsonSerializationException("Expected date object value.");
            }

            DateTime utcTime = ((DateTime)value).ToUniversalTime();

            writer.WriteRawValue(JsonConvert.ToString(utcTime, DateFormatHandling.MicrosoftDateFormat, DateTimeZoneHandling.Utc));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
