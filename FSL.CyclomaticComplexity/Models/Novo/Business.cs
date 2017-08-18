using FSL.CyclomaticComplexity.Models.Novo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FSL.CyclomaticComplexity.Models.Novo
{
    public sealed class Business
    {
        private readonly IHelperService _helperService;
        private readonly IFactory _factory;

        public Business(IHelperService helperService,
            IFactory factory)
        {
            _factory = factory;
            _helperService = helperService;
        }

        public string TrNegociacaoPeriodo(IQueryable<Negociacao> negociacoes, string titulo, TipoData tipoData, bool anteriorHoje)
        {
            var perfilNestaNegociacao = PerfilNegociacao.Comprador;
            var dataHorarioNegociacao = default(DateTime);
            var horarioNegociacao = default(DateTime);
            var status = StatusNegociacao.None;
            var sbTemp = new StringBuilder();
            var numeroLinhaNegociacao = 0;
            var primeiraLinha = 1;
            var limiteLinha = 100;
            var statusIgnorados = RetornarStatusIgnorados();

            foreach (var negociacao in negociacoes)
            {
                horarioNegociacao = ResolverHorarioNegociacao(negociacao.HorarioNegociacao);
                dataHorarioNegociacao = ResolverDataHorarioNegociacao(negociacao.DataNegociacao, horarioNegociacao);
                status = ResolverStatusNegociacao(dataHorarioNegociacao, negociacao.IdNegociacao, negociacao.DbStatus, negociacao.FlagVendedorTeveNegociacoes, negociacao.FlagCompradorTeveNegociacoes);
                perfilNestaNegociacao = ResolverPerfilNegociacao(negociacao.IdNegociacao);

                numeroLinhaNegociacao = 0;
                if (!statusIgnorados.Contains(status))
                {
                    var table = _factory.InstanceOf<TableTag>();
                    
                    numeroLinhaNegociacao++;
                    if (numeroLinhaNegociacao == primeiraLinha)
                    {
                        table.AddCssClass("profileTextUser");
                    }

                    if (numeroLinhaNegociacao <= limiteLinha)
                    {
                        var tr = _factory.InstanceOf<TrTag>();
                        if (_helperService.EhImpar(numeroLinhaNegociacao))
                        {
                            tr.AddCssClass("linhaimpar");
                        }
                        else
                        {
                            tr.AddCssClass("linhapar");
                        }

                        var resultadoNegociacao = _factory.InstanceOf<IStatusNegociacaoService>(status).MontarResultadoNegociacao(negociacao, perfilNestaNegociacao, titulo);

                        var td1 = _factory.InstanceOf<TdTag>().BuildTag(resultadoNegociacao.Html);
                        var td2 = _factory.InstanceOf<TdTag>()
                            .AddCssClass("profileTextUser menorUSER nowrap")
                            .BuildTag(_factory.InstanceOf<ITipoDataService>(tipoData).FormatarDataHoraNegociacao(dataHorarioNegociacao));

                        var anchor = BuildVisualizacaoAnchor(perfilNestaNegociacao, negociacao);
                        var h51 = BuildConfirmacaoH5(perfilNestaNegociacao, status, negociacao);
                        var h52 = _factory.InstanceOf<H5Tag>().BuildTag(resultadoNegociacao.Acao);
                        var td3 = _factory.InstanceOf<TdTag>().BuildTag(anchor, h51, h52);

                        tr.BuildTag(td1, td2, td3);
                        table.BuildTag(tr);
                    }

                    sbTemp.AppendLine(table.RenderTag());
                }
            }

            return sbTemp.ToString();
        }

        private Tag BuildConfirmacaoH5(PerfilNegociacao perfilNestaNegociacao, StatusNegociacao status, Negociacao negociacao)
        {
            var h51Html = new StringBuilder();
            if (status == StatusNegociacao.CompradorAceito)
            {
                h51Html.Append("confirmada, ");
            }
            if (perfilNestaNegociacao == PerfilNegociacao.Vendedor)
            {
                h51Html.Append("a " + _helperService.FormatarNomeUsuario(negociacao.IdComprador).Replace("<a", "<a class='link-ir-para-pagina'"));
            }
            else
            {
                h51Html.Append("de " + _helperService.FormatarNomeUsuario(negociacao.IdVendedor).Replace("<a", "<a class='link-ir-para-pagina'"));
            }

            return _factory.InstanceOf<H5Tag>().BuildTag(h51Html.ToString());
        }

        private Tag BuildVisualizacaoAnchor(PerfilNegociacao perfilNestaNegociacao, Negociacao negociacao)
        {
            var anchor = _factory.InstanceOf<AnchorTag>();
            anchor.MergeAttribute("title", "Visualizar esta negociação");
            anchor.AddCssClass("link-ir-para-pagina");
            anchor.MergeAttribute("href", "MinhaNegociacao.aspx?Codigo=" + negociacao.Id);
            var anchorInnerHtml = new StringBuilder();

            if (negociacao.Titulo != null && perfilNestaNegociacao == PerfilNegociacao.Vendedor)
            {
                anchorInnerHtml.Append(negociacao.Titulo);
            }
            else
            {
                if (perfilNestaNegociacao == PerfilNegociacao.Vendedor)
                {
                    anchorInnerHtml.Append(_helperService.Limitar(_helperService.FormatarNomeLocal(negociacao.LocalPartida, _helperService.RetornarUserName(), "local"), 30) + ",");
                    anchorInnerHtml.Append(_helperService.Limitar(_helperService.FormatarNomeLocal(negociacao.LocalChegada, _helperService.RetornarUserName(), "local"), 30));

                    if (negociacao.TipoOpcao == "emprestimo")
                    {
                        dynamic negociacaovolta = (from item in _helperService.RetornarNegociacoesPorLocais() where item.CodigoId == negociacao.IdItemInverso select item);

                        if (negociacao.LocalPartida == negociacaovolta.strLocal)
                        {
                            anchorInnerHtml.Append(" <span class='menorUSER'>(ida e volta)</span>");
                        }
                        else
                        {
                            anchorInnerHtml.Append("," + _helperService.Limitar(_helperService.FormatarNomeLocal(negociacaovolta.strLocal, _helperService.RetornarUserName(), "local"), 30));
                        }
                    }
                }
                else
                {
                    anchorInnerHtml.Append(negociacao.TituloComprador);
                }
            }

            return anchor.BuildTag(anchorInnerHtml.ToString());
        }

        private DateTime ResolverDataHorarioNegociacao(DateTime datNegociacao, DateTime horarioNegociacao)
        {
            return Convert.ToDateTime(datNegociacao + " " + horarioNegociacao.ToShortTimeString());
        }

        private static List<StatusNegociacao> RetornarStatusIgnorados()
        {
            return new List<StatusNegociacao>
            {
                StatusNegociacao.ExpiradoSemNegociacao,
                StatusNegociacao.Recusado,
                StatusNegociacao.ExpiradoCancelado,
                StatusNegociacao.ExpiradoFalho
            };
        }

        private DateTime ResolverHorarioNegociacao(DateTime datHorarioNegociacao)
        {
            if (datHorarioNegociacao != default(DateTime))
            {
                return datHorarioNegociacao;
            }
            else
            {
                return DateTime.Now;
            }
        }

        private PerfilNegociacao ResolverPerfilNegociacao(string idVendedor)
        {
            if (idVendedor == _helperService.RetornarUserName())
            {
                return PerfilNegociacao.Vendedor;
            }
            else
            {
                return PerfilNegociacao.Comprador;
            }
        }

        private StatusNegociacao ResolverStatusNegociacao(DateTime dataHoraNegociacao, 
            string idNegociacao, 
            StatusNegociacao dbStatus, 
            bool vendedorTeveNegociacoes,
            bool? compradorTeveNegociacoes)
        {
            if (dataHoraNegociacao > DateTime.Now)
            {
                return dbStatus;
            }

            if (idNegociacao == null)
            {
                return StatusNegociacao.ExpiradoSemNegociacao;
            }

            compradorTeveNegociacoes = compradorTeveNegociacoes.HasValue ? compradorTeveNegociacoes.Value : false;
            var naoTeveNegociacoes = (!vendedorTeveNegociacoes || !compradorTeveNegociacoes.Value);
            var ehExpiradoCancelado = (dbStatus == StatusNegociacao.ExpiradoCancelado);
            if (ehExpiradoCancelado || naoTeveNegociacoes)
            {
                return StatusNegociacao.ExpiradoFalho;
            }

            if (vendedorTeveNegociacoes)
            {
                return StatusNegociacao.ExpiradoConcluido;
            }

            return StatusNegociacao.ExpiradoPendente;
        }
    }
}