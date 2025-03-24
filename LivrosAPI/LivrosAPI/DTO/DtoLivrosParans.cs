using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace LivrosAPI.DTO
{
    public class DtoLivrosParans
    {
        [Display(Name = "Identificador do Livro")]
        [SwaggerSchema(Description = "Identificador do Livro")]
        public int? id { get; set; }

        [Display(Name = "Nome do Livro")]
        [SwaggerSchema(Description = "Nome do Livro")]
        public string Nome { get; set; }

        [Display(Name = "Nome do Autor")]
        [SwaggerSchema(Description = "Nome do Autor")]
        public string Autor { get; set; }

        [Display(Name = "Preço Máximo do Livro")]
        [SwaggerSchema(Description = "Preço Máximo do Livro")]
        public decimal? Valor { get; set; }

        [Display(Name = "Ilustrador do Livro")]
        [SwaggerSchema(Description = "Ilustrador do Livro")]
        public string Ilustrador { get; set; }

        [Display(Name = "Gênero do Livro")]
        [SwaggerSchema(Description = "Gênero do Livro")]
        public string Genero { get; set; }

        [Display(Name = "Ordenar Por (asc/desc)")]
        [SwaggerSchema(Description = "Ordenar Por (asc/desc)")]
        public string OrdenarPor { get; set; }
    }
}
