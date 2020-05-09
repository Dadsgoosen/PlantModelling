using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using PlantSimulatorService.Simulations.Model.Options;

namespace PlantSimulatorService.Converters
{
    public class RangeJsonConverter: JsonConverter<Range<int>>
    {
        private static readonly Type T = typeof(int[]);

        public override Range<int> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            IList<int> values = new List<int>(2);
            
            while (reader.Read() && values.Count < 2)
            {
                if (reader.TokenType == JsonTokenType.Number)
                {
                    values.Add(reader.GetInt32());
                }
            }

            return values.ToArray();
        }

        public override void Write(Utf8JsonWriter writer, Range<int> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            writer.WriteNumberValue(value.Min);
            writer.WriteNumberValue(value.Max);
            writer.WriteEndArray();
        }
    }
}