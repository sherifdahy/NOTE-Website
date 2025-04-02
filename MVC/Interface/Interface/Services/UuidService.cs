using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json.Serialization;
using Entities.Models.Document.BaseComponent;
using Entities.Models.Document.Receipt;

namespace Interface.Services
{
    public class UuidService
    {
        public static string GenerateUUID(Receipt receipt)
        {
            return Hash(serialize_text(JsonConvert.SerializeObject(receipt, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore 
            })));
        }

        

        public static string Hash(string input)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                string hexString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hexString;
            }
        }

        public static string serialize_text(string json)
        {
            JObject request = JsonConvert.DeserializeObject<JObject>(json, new JsonSerializerSettings()
            {
                FloatFormatHandling = FloatFormatHandling.String,
                FloatParseHandling = FloatParseHandling.Decimal,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateParseHandling = DateParseHandling.None
            });

            String canonicalString = SerializeJSONToken(request);
            return canonicalString;
        }
        public static string SerializeJSONToken(JToken request)
        {
            string serialized = "";
            if (request.Parent is null)
            {
                SerializeJSONToken(request.First);
            }
            else
            {
                if (request.Type == JTokenType.Property)
                {
                    string name = ((JProperty)request).Name.ToUpper();
                    serialized += "\"" + name + "\"";
                    foreach (var property in request)
                    {
                        if (property.Type == JTokenType.Object)
                        {
                            serialized += SerializeJSONToken(property);
                        }
                        if (property.Type == JTokenType.Boolean || property.Type == JTokenType.Integer || property.Type == JTokenType.Float || property.Type == JTokenType.Date)
                        {
                            serialized += "\"" + property.Value<string>() + "\"";
                        }
                        if (property.Type == JTokenType.String)
                        {
                            serialized += JsonConvert.ToString(property.Value<string>());
                        }
                        if (property.Type == JTokenType.Array)
                        {
                            foreach (var item in property.Children())
                            {
                                serialized += "\"" + ((JProperty)request).Name.ToUpper() + "\"";
                                serialized += SerializeJSONToken(item);
                            }
                        }
                    }
                }
                if (request.Type == JTokenType.String)
                {
                    serialized += JsonConvert.ToString(request.Value<string>());
                }
            }
            if (request.Type == JTokenType.Object)
            {
                foreach (var property in request.Children())
                {

                    if (property.Type == JTokenType.Object || property.Type == JTokenType.Property)
                    {
                        serialized += SerializeJSONToken(property);
                    }
                }
            }

            return serialized;
        }

    }
}
