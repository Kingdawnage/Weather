using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Weather.mvvm.model
{
    public class CustomJsonToDateTime : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (DateTime.TryParse(reader.GetString(), out var result))
            {
                return result;
            }
            else
            {
                throw new InvalidDataException("Invalid DateTime format...");
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
