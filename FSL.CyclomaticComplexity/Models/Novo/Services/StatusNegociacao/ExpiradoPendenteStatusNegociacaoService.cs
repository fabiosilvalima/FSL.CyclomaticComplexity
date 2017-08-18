using System.Text;


namespace FSL.CyclomaticComplexity.Models.Novo.Services
{
    public class ExpiradoPendenteStatusNegociacaoService : IStatusNegociacaoService
    {
        private readonly IHelperService _helperService;
        private readonly IFactory _factory;

        public ExpiradoPendenteStatusNegociacaoService(IHelperService helperService,
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
                sbTemp.Append(_helperService.RetornarIconeNegociacao("Vendedor_vermelho", "Negociação que seria DADA por você ainda NÃO CONFIRMADA"));
                result.Acao = "(negociação aconteceu ? <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.Id + "&action=vendedor_confirmado'>sim</a> | <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.Id + "&action=vendedor_negado'>não</a>)";
            }
            else
            {
                if (negociacao.FlagCompradorTeveNegociacoes == true)
                {
                    sbTemp.Append(_helperService.RetornarIconeNegociacao("Comprador_amarelo", "Negociação que foi RECEBIDA por você ainda NÃO CONFIRMADA pelo vendedor"));
                    result.Acao = "(aguardando confirmação)";
                }
                else
                {
                    sbTemp.Append(_helperService.RetornarIconeNegociacao("Comprador_vermelho", "Negociação que seria RECEBIDA por você ainda NÃO CONFIRMADA"));
                    result.Acao = "(negociação aconteceu ? <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.Id + "&action=Comprador_confirmado'>sim</a> | <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.Id + "&action=Comprador_negado'>não</a>)";
                }
            }

            result.Html = sbTemp.ToString();

            return result;
        }
    }
}