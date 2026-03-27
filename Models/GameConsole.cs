using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameBrainExplorer.Models
{
    public class GameConsole
    {
        public int Id { get; set; }

        // Nullable int to safely handle missing or invalid values
        [JsonConverter(typeof(IntOrStringNullableConverter))]
        public int? Year { get; set; }

        public string Name { get; set; }
        public string Genre { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public Rating Rating { get; set; }
        public bool Adult_Only { get; set; }
        public List<string> Screenshots { get; set; }
        public string Micro_Trailer { get; set; }
        public string Gameplay { get; set; }
        public string Short_Description { get; set; }
    }

    // Converter to handle both string and number, nullable
    public class IntOrStringNullableConverter : JsonConverter<int?>
    {
        public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // ✅ CASE 1: Number (including 2025.0)
            if (reader.TokenType == JsonTokenType.Number)
            {
                double value = reader.GetDouble(); // FIX HERE
                return (int)value; // convert to int
            }

            // ✅ CASE 2: String ("2025")
            if (reader.TokenType == JsonTokenType.String)
            {
                var str = reader.GetString();

                if (double.TryParse(str, out double value))
                    return (int)value;
            }

            return null; // fallback
        }

        public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteNumberValue(value.Value);
            else
                writer.WriteNullValue();
        }
    }
}