using LivrosAPI.DTO;
using LivrosAPI.Models;
using LivrosAPI.Repositories.IRepositories;
using System.Text.Json;

namespace LivrosAPI.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly List<Livro> _livros;

        public LivroRepository()
        {
            var json = File.ReadAllText("Data/books.json");
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
            _livros = JsonSerializer.Deserialize<List<Livro>>(json, options);
        }

        public IEnumerable<Livro> BuscarLivros(DtoLivrosParans parametros)
        {
            var result = _livros.AsQueryable();

            if (parametros.id != null && parametros.id > 0)
            {
                result = result.Where(l => l.Id == parametros.id);
            }

            if (!string.IsNullOrEmpty(parametros.Nome))
            {
                result = result.Where(l => l.Name.Contains(parametros.Nome, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(parametros.Autor))
            {
                result = result.Where(l => l.Specifications.Author.Contains(parametros.Autor, StringComparison.OrdinalIgnoreCase));
            }

            if (parametros.Valor != null && parametros.Valor > 0)
            {
                result = result.Where(l => l.Price <= parametros.Valor);
            }

            if (!string.IsNullOrEmpty(parametros.Ilustrador))
            {
                result = result.Where(l => l.Specifications.Illustrator != null && l.Specifications.Illustrator.Any(i => i.Contains(parametros.Ilustrador, StringComparison.OrdinalIgnoreCase)));
            }

            if (!string.IsNullOrEmpty(parametros.Genero))
            {
                result = result.Where(l => l.Specifications.Genres != null && l.Specifications.Genres.Any(i => i.Contains(parametros.Genero, StringComparison.OrdinalIgnoreCase)));
            }

            switch (parametros.OrdenarPor)
            {
                case "desc":
                    result = result.OrderByDescending(l => l.Price);
                    break;
                default:
                    result = result.OrderBy(l => l.Price);
                    break;
            }

            return result;
        }

        public Livro BuscarPorId(int id)
        {
            return _livros.FirstOrDefault(l => l.Id == id);
        }

    }
}
