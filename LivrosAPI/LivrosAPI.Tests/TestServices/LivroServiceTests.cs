using LivrosAPI.Services;

public class LivroServiceTests
{
    private readonly LivroService _service;

    public LivroServiceTests()
    {
        _service = new LivroService();
    }

    [Theory]
    [InlineData(100, 20)]
    [InlineData(50, 10)]
    [InlineData(200, 40)]
    [InlineData(0, 0)]
    public void CalcularFrete_DeveRetornarValorCorreto(decimal precoLivro, decimal freteEsperado)
    {
        var resultado = _service.CalcularFrete(precoLivro);
        Assert.Equal(freteEsperado, resultado);
    }
}
