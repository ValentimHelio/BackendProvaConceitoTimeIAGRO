using LivrosAPI.DTO;
using LivrosAPI.Models;
using LivrosAPI.Repositories;

public class LivroRepositoryTests
{
    private readonly LivroRepository _repository;
    private readonly List<Livro> _mockLivros;

    public LivroRepositoryTests()
    {
        _mockLivros = new List<Livro>
        {
            new Livro { Id = 1, Name = "Livro A", Price = 50, Specifications = new Especificacoes { Author = "Autor 1", Genres = new List<string>{ "Ficção" }, Illustrator = new List<string>{ "Ilustrador 1" } } },
            new Livro { Id = 2, Name = "Livro B", Price = 30, Specifications = new Especificacoes { Author = "Autor 2", Genres = new List<string>{ "Fantasia" }, Illustrator = new List<string>{ "Ilustrador 2" } } },
            new Livro { Id = 3, Name = "Livro C", Price = 80, Specifications = new Especificacoes { Author = "Autor 3", Genres = new List<string>{ "Mistério" }, Illustrator = new List<string>{ "Ilustrador 3" } } }
        };

        _repository = new LivroRepository();
        typeof(LivroRepository).GetField("_livros", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(_repository, _mockLivros);
    }

    [Fact]
    public void BuscarLivros_DeveRetornarTodosOsLivros_QuandoNaoHouverFiltro()
    {
        var parametros = new DtoLivrosParans();
        var resultado = _repository.BuscarLivros(parametros).ToList();

        Assert.Equal(3, resultado.Count);
    }

    [Fact]
    public void BuscarLivros_DeveFiltrarPorId()
    {
        var parametros = new DtoLivrosParans { id = 1 };
        var resultado = _repository.BuscarLivros(parametros).ToList();

        Assert.Single(resultado);
        Assert.Equal(1, resultado[0].Id);
    }

    [Fact]
    public void BuscarLivros_DeveFiltrarPorNome()
    {
        var parametros = new DtoLivrosParans { Nome = "Livro A" };
        var resultado = _repository.BuscarLivros(parametros).ToList();

        Assert.Single(resultado);
        Assert.Equal("Livro A", resultado[0].Name);
    }

    [Fact]
    public void BuscarLivros_DeveFiltrarPorAutor()
    {
        var parametros = new DtoLivrosParans { Autor = "Autor 2" };
        var resultado = _repository.BuscarLivros(parametros).ToList();

        Assert.Single(resultado);
        Assert.Equal("Autor 2", resultado[0].Specifications.Author);
    }

    [Fact]
    public void BuscarLivros_DeveFiltrarPorPreco()
    {
        var parametros = new DtoLivrosParans { Valor = 50 };
        var resultado = _repository.BuscarLivros(parametros).ToList();

        Assert.Equal(2, resultado.Count);
    }

    [Fact]
    public void BuscarLivros_DeveOrdenarPorPrecoAscendente()
    {
        var parametros = new DtoLivrosParans { OrdenarPor = "asc" };
        var resultado = _repository.BuscarLivros(parametros).ToList();

        Assert.Equal(3, resultado.Count);
        Assert.Equal(30, resultado[0].Price);
        Assert.Equal(50, resultado[1].Price);
        Assert.Equal(80, resultado[2].Price);
    }

    [Fact]
    public void BuscarLivros_DeveOrdenarPorPrecoDescendente()
    {
        var parametros = new DtoLivrosParans { OrdenarPor = "desc" };
        var resultado = _repository.BuscarLivros(parametros).ToList();

        Assert.Equal(3, resultado.Count);
        Assert.Equal(80, resultado[0].Price);
        Assert.Equal(50, resultado[1].Price);
        Assert.Equal(30, resultado[2].Price);
    }

    [Fact]
    public void BuscarPorId_DeveRetornarLivroCorreto()
    {
        var resultado = _repository.BuscarPorId(2);

        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Id);
    }

    [Fact]
    public void BuscarPorId_DeveRetornarNull_SeLivroNaoExistir()
    {
        var resultado = _repository.BuscarPorId(99);

        Assert.Null(resultado);
    }
}
