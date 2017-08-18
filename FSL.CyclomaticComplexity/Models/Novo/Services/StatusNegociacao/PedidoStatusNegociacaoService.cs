using System.Text;


namespace FSL.CyclomaticComplexity.Models.Novo.Services
{
    public class PedidoStatusNegociacaoService : IStatusNegociacaoService
    {
        private readonly IHelperService _helperService;
        private readonly IFactory _factory;

        public PedidoStatusNegociacaoService(IHelperService helperService,
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
                sbTemp.Append(_helperService.RetornarIconeNegociacao("Vendedor_vermelho", "Negociação a ser DADA por você, aguardando SUA aprovação"));
                result.Acao = "(<a href='MinhaNegociacao.aspx?Codigo=" + negociacao.Id + "&action=vendedor_aceito'>aceitar</a> | <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.Id + "&action=vendedor_recusado'>recusar</a> | <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.Id + "'>ver mais detalhes</a>)";
            }
            else
            {
                sbTemp.Append(_helperService.RetornarIconeNegociacao("Comprador_amarelo", "Negociação a ser RECEBIDA por você, aguardando a aprovação DO vendedor"));
                result.Acao = "(aguardando aprovação)";
            }

            result.Html = sbTemp.ToString();

            return result;
        }
    }
}