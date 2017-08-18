using System.Text;


namespace FSL.CyclomaticComplexity.Models.Novo.Services
{
    public class ExpiradoConcluidoStatusNegociacaoService : IStatusNegociacaoService
    {
        private readonly IHelperService _helperService;
        private readonly IFactory _factory;

        public ExpiradoConcluidoStatusNegociacaoService(IHelperService helperService,
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
                sbTemp.Append(_helperService.RetornarIconeNegociacao("Vendedor_concluido", "Negociacao DADA por você CONCLUÍDA com sucesso, com negociacoes resgatadas"));
                result.Acao = "(" + negociacao.ValorMoeda + " <img src='..Imagens/Negociacao.png' /> " + _helperService.AplicarPlural("resgatada", negociacao.ValorMoeda) + ")";
            }
            else
            {
                if (negociacao.FlagCompradorTeveNegociacoes == true)
                {
                    sbTemp.Append(_helperService.RetornarIconeNegociacao("Comprador_concluido", "Negociação RECEBIDA por você CONCLUÍDA com sucesso"));
                }
                else if (negociacao.FlagCompradorTeveNegociacoes == null)
                {
                    sbTemp.Append(_helperService.RetornarIconeNegociacao("Comprador_vermelho", "Negociação que seria RECEBIDA por você ainda NÃO confirmada"));
                    result.Acao = "(negociacao aconteceu ? <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.Id + "&action=Comprador_confirmado'>sim</a> | <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.Id + "&action=Comprador_negado'>não</a>)";
                }
                else
                {
                    sbTemp.Append(_helperService.RetornarIconeNegociacao("Comprador_concluido", "Negociação RECEBIDA por você CONCLUÍDA com sucesso"));
                    result.Acao = "(vendedor inseriu negociação confirmando)";
                }
            }

            result.Html = sbTemp.ToString();

            return result;
        }
    }
}