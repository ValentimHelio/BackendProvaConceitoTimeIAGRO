using LivrosAPI.Controllers;
using LivrosAPI.DTO;
using LivrosAPI.Models;
using LivrosAPI.Repositories.IRepositories;
using LivrosAPI.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LivrosAPI.Tests.Controllers
{
    public class LivroControllerTests
    {
        private readonly Mock<ILivroRepository> _repositoryMock;
        private readonly Mock<ILivroService> _serviceMock;
        private readonly LivroController _controller;

        public LivroControllerTests()
        {
            _repositoryMock = new Mock<ILivroRepository>();
            _serviceMock = new Mock<ILivroService>();

            _controller = new LivroController(_repositoryMock.Object, _serviceMock.Object);
        }

        [Fact]
        public void BuscarLivros_DeveRetornarOk_QuandoLivrosForemEncontrados()
        {
            var parametros = new DtoLivrosParans { Nome = "Harry Potter" };
            var livros = new List<Livro>
            {
                new Livro { Id = 1, Name = "Harry Potter e a Pedra Filosofal", Price = 9.99M }
            };

            _repositoryMock.Setup(repo => repo.BuscarLivros(parametros))
                           .Returns(livros.AsQueryable());

            var resultado = _controller.BuscarLivros(parametros);

            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var resultadoLivros = Assert.IsType<List<Livro>>(okResult.Value);
            Assert.Single(resultadoLivros);
        }

        [Fact]
        public void BuscarLivros_DeveRetornarNotFound_QuandoNaoHouverLivros()
        {
            var parametros = new DtoLivrosParans { Nome = "Desconhecido" };

            _repositoryMock.Setup(repo => repo.BuscarLivros(parametros))
                           .Returns(Enumerable.Empty<Livro>().AsQueryable());

            var resultado = _controller.BuscarLivros(parametros);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultado.Result);
            Assert.Equal("Não foi encontrado nenhum livro.", notFoundResult.Value);
        }

        [Fact]
        public void CalcularFrete_DeveRetornarValorCorreto()
        {
            var livro = new Livro { Id = 1, Name = "Livro Teste", Price = 50.00M };
            _repositoryMock.Setup(repo => repo.BuscarPorId(1)).Returns(livro);
            _serviceMock.Setup(service => service.CalcularFrete(livro.Price)).Returns(10.00M);

            var resultado = _controller.CalcularFrete(1);

            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            Assert.Contains("O valor do Frete será:", okResult.Value.ToString());
        }

        [Fact]
        public void CalcularFrete_DeveRetornarNotFound_QuandoLivroNaoExiste()
        {
            _repositoryMock.Setup(repo => repo.BuscarPorId(99)).Returns((Livro)null);

            var resultado = _controller.CalcularFrete(99);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultado.Result);
            Assert.Equal("Livro não encontrado", notFoundResult.Value);
        }
    }
}
