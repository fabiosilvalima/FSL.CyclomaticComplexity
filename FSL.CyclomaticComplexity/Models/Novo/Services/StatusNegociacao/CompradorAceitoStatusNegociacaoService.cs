using System.Text;


namespace FSL.CyclomaticComplexity.Models.Novo.Services
{
    public class CompradorAceitoStatusNegociacaoService : IStatusNegociacaoService
    {
        private readonly IHelperService _helperService;
        private readonly IFactory _factory;

        public CompradorAceitoStatusNegociacaoService(IHelperService helperService, 
            IFactory factory)
        {
            _factory = factory;
            _helperService = helperService;
        }

        public StatusNegociacaoResult MontarResultadoNegociacao(Negociacao negociacao, PerfilNegociacao perfilNestaNegociacao, string titulo)
        {
            var result = _factory.InstanceOf<StatusNegociacaoResult>();
            var sbTemp = new StringBuilder();

            if (perfilNestaNegociacao == PerfilNegociacao.Vendedor)
            {
                sbTemp.Append(_helperService.RetornarIconeNegociacao("Vendedor_verde", "Negociação a ser DADA por você"));
            }
            else
            {
                sbTemp.Append(_helperService.RetornarIconeNegociacao("Comprador_verde", "Negociação a ser RECEBIDA por você"));
            }
            if (titulo == "hoje" | titulo == "amanhã")
            {
                result.Acao = "(veja os <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.Id + "'>detalhes</a> p/ se programar)";
            }

            result.Html = sbTemp.ToString();

            return result;
        }
    }
}