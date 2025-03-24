using LivrosAPI.DTO;
using LivrosAPI.Models;
using LivrosAPI.Repositories.IRepositories;
using LivrosAPI.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace LivrosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _repository;
        private readonly ILivroService _service;

        public LivroController(ILivroRepository repository, ILivroService service)
        {
            _repository = repository;
            _service = service;
        }

        [HttpGet("BuscarLivros")]
        public ActionResult<Livro> BuscarLivros([FromQuery] DtoLivrosParans parametros)
        {
            try
            {
                var livro = _repository.BuscarLivros(parametros);
                if (livro.ToList().Count().Equals(0)) { return NotFound("Não foi encontrado nenhum livro."); }
                return Ok(livro.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("CalcularFrete/{id}")]
        public ActionResult<string> CalcularFrete(int id)
        {
            try
            {
                var livro = _repository.BuscarPorId(id);
                if (livro == null) { return NotFound("Livro não encontrado"); }

                var valorFrete = _service.CalcularFrete(livro.Price);
                return Ok($" O valor do Frete será: " + valorFrete.ToString("C", new System.Globalization.CultureInfo("en-US")));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}

