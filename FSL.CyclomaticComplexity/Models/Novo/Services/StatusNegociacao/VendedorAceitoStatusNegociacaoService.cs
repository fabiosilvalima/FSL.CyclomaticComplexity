using System.Text;


namespace FSL.CyclomaticComplexity.Models.Novo.Services
{
    public class VendedorAceitoStatusNegociacaoService : IStatusNegociacaoService
    {
        private readonly IHelperService _helperService;
        private readonly IFactory _factory;

        public VendedorAceitoStatusNegociacaoService(IHelperService helperService,
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
                sbTemp.Append(_helperService.RetornarIconeNegociacao("Vendedor_amarelo", "Negociação a ser DADA por você, aguardando a confirmação DO Comprador"));
                result.Acao = "(aguardando confirmação)";
            }
            else
            {
                sbTemp.Append(_helperService.RetornarIconeNegociacao("Comprador_vermelho", "Negociação a ser RECEBIDA por você, aguardando SUA confirmação"));
                result.Acao = "(<a href='MinhaNegociacao.aspx?Codigo=" + negociacao.Id + "&action=Comprador_aceito'>aceitar</a> | <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.Id + "&action=Comprador_recusado'>recusar</a> | <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.Id + "'>ver mais detalhes</a>)";
            }

            result.Html = sbTemp.ToString();

            return result;
        }
    }
}