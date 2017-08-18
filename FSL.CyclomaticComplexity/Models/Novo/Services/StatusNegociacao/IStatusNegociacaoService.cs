
namespace FSL.CyclomaticComplexity.Models.Novo.Services
{
    public interface IStatusNegociacaoService
    {
        StatusNegociacaoResult MontarResultadoNegociacao(Negociacao negociacao, PerfilNegociacao perfilNestaNegociacao, string titulo);
    }
}