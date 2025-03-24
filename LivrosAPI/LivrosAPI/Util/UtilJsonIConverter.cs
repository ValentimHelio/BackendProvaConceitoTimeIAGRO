using System.Text.Json;
using System.Text.Json.Serialization;

public class UtilJsonIConverter : JsonConverter<List<string>>
{
    public override List<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            // Se for uma string única, converte para uma lista com um único item
            return new List<string> { reader.GetString() };
        }
        else if (reader.TokenType == JsonTokenType.StartArray)
        {
            // Se for um array, desserializa normalmente
            var list = new List<string>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                    break;
                if (reader.TokenType == JsonTokenType.String)
                    list.Add(reader.GetString());
            }
            return list;
        }
        else
        {
            throw new JsonException("Formato inválido para Illustrator");
        }
    }

    public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}
