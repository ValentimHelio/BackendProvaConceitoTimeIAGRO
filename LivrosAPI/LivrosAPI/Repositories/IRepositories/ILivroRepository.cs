using LivrosAPI.DTO;
using LivrosAPI.Models;

namespace LivrosAPI.Repositories.IRepositories;

public interface ILivroRepository
{
    public IEnumerable<Livro> BuscarLivros(DtoLivrosParans parametros);
    public Livro BuscarPorId(int id);
}
