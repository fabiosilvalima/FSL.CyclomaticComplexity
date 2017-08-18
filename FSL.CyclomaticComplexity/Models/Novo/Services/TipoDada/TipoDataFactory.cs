
namespace FSL.CyclomaticComplexity.Models.Novo.Services
{
    // se necessario ser especifico
    public class TipoDataFactory : Factory, ITipoDataFactory
    {
        public ITipoDataService InstanceOf(TipoData tipodata)
        {
            return InstanceOf<ITipoDataService>(tipodata);
        }
    }
}