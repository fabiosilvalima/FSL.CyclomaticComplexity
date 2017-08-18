
namespace FSL.CyclomaticComplexity.Models.Novo.Services
{
    // se necessario ser especifico
    public class StatusNegociacaoFactory : Factory, IStatusNegociacaoFactory
    {
        public IStatusNegociacaoService InstanceOf(StatusNegociacao statusNegociacao)
        {
            return InstanceOf<IStatusNegociacaoService>(statusNegociacao);
        }
    }
}