namespace LivrosAPI.Models;

public class Livro
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Especificacoes Specifications { get; set; }
}
