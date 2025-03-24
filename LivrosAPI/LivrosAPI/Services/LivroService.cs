using LivrosAPI.Services.IService;

namespace LivrosAPI.Services
{
    public class LivroService: ILivroService
    {
        public decimal CalcularFrete(decimal precoLivro) => precoLivro * 0.2m;
    }
}
