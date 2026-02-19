using System.Text.Json;
using System.Text.Json.Serialization;

namespace MasterCompany.Domain.Models
{
    public class SafeDateTimeConverter : JsonConverter<DateTime?> /// This class defines a custom JSON converter for nullable DateTime properties, allowing for safe parsing and formatting of date values in JSON. 
    {   /// The Read method is responsible for deserializing a JSON string into a nullable DateTime object.
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (DateTime.TryParse(value, out var date))
                return date;

            return null;
        }

        /// The Write method is responsible for serializing a nullable DateTime object into a JSON string. If the nullable DateTime has a value, it formats the date as a string in the "yyyy-MM-dd" format and writes it to the JSON output. If the nullable DateTime does not have a value (i.e., it is null), it writes a null value to the JSON output, ensuring that the absence of a date is correctly represented in the serialized JSON.

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.Value.ToString("yyyy-MM-dd"));
            else
                writer.WriteNullValue();
        }
    }
}