using System.Text.Json.Serialization;

namespace LivrosAPI.Models;

public class Especificacoes
{
    [JsonPropertyName("Originally published")]
    public string OriginallyPublished { get; set; }
    public string Author { get; set; }

    [JsonPropertyName("Page count")]
    public int PageCount { get; set; }

    [JsonConverter(typeof(UtilJsonIConverter))]
    public List<string> Illustrator { get; set; }

    [JsonConverter(typeof(UtilJsonIConverter))]
    public List<string> Genres { get; set; }
}
