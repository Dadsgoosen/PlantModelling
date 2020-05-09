using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PlantSimulatorService.Converters
{
    public class VectorJsonConverter : JsonConverter<Vector2>
    {
        public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            IList<int> values = new List<int>(2);

            while (reader.Read() && values.Count < 2)
            {
                if (reader.TokenType == JsonTokenType.Number)
                {
                    values.Add(reader.GetInt32());
                }
            }

            return new Vector2(values[0], values[1]); 
        }

        public override void Write(Utf8JsonWriter writer, Vector2 value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            writer.WriteNumberValue(value.X);
            writer.WriteNumberValue(value.Y);
            writer.WriteEndArray();
        }
    }
}