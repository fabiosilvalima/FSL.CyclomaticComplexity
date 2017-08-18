using System;
using System.Linq;
using System.Text;

namespace FSL.CyclomaticComplexity.Models.Original
{
    public class Business
    {
        public string TrNegociacaoPeriodo(IQueryable<Negociacao> negociacoes, string titulo, string tipodata, bool anteriorahoje)
        {
            string perfilNestaNegociacao = "";
            DateTime dataNula = default(DateTime);
            DateTime dataehorarioNegociacao = default(DateTime);
            DateTime horarioNegociacao = default(DateTime);
            string status = null;
            
            StringBuilder sbTemp = new StringBuilder();
            foreach (var negociacao in negociacoes)
            {
                if (negociacao.datHorarioNegociacao.CompareTo(dataNula) != 0)
                {
                    horarioNegociacao = negociacao.datHorarioNegociacao;
                }
                else
                {
                    horarioNegociacao = DateTime.Now;
                }
                dataehorarioNegociacao = Convert.ToDateTime(negociacao.datNegociacao + " " + horarioNegociacao.ToShortTimeString());
                if (dataehorarioNegociacao <= DateTime.Now)
                {
                    if (negociacao.IDNegociacao != null)
                    {
                        if (negociacao.dbStatus == "_cancelado")
                        {
                            status = "expirado_falho";
                        }
                        else
                        {
                            if (negociacao.bitVendedorTeveNegociacoes == true)
                            {
                                status = "expirado_concluído";
                            }
                            else
                            {
                                if (negociacao.bitVendedorTeveNegociacoes == false || negociacao.bitCompradorTeveNegociacoes == false)
                                {
                                    status = "expirado_falho";
                                }
                                else
                                {
                                    status = "expirado_pendente";
                                }
                            }
                        }
                    }
                    else
                    {
                        status = "expirado sem negociacao";
                    }
                }
                else
                {
                    status = negociacao.dbStatus;
                }
                
                if (negociacao.IDVendedor.ToString() == Helper.UserName())
                {
                    perfilNestaNegociacao = "vendedor";
                }
                else
                {
                    perfilNestaNegociacao = "comprador";
                }

                var numeroLinhaNegociacao = 0;
                if (status != "expirado sem negociacao" && status != "_recusado" && status != "_cancelado" && status != "_falho")
                {
                    numeroLinhaNegociacao += 1;
                    if (numeroLinhaNegociacao == 1)
                    {
                        sbTemp.AppendLine("<table class='profileTextUser'>");
                    }

                    if (numeroLinhaNegociacao <= 100) 
                    {
                        sbTemp.Append("  <tr class='");
                        if (Helper.ehImpar(numeroLinhaNegociacao))
                        {
                            sbTemp.Append("linhaimpar");
                        }
                        else
                        {
                            sbTemp.Append("linhapar");
                        }
                        sbTemp.AppendLine("'>");
                        sbTemp.AppendLine("      <td>");
                        string acao = "";
                        switch (status)
                        {
                            case "expirado_concluído":
                                if (perfilNestaNegociacao == "vendedor")
                                {
                                    sbTemp.Append(Helper.iconeNegociacao("Vendedor_concluido", "Negociacao DADA por você CONCLUÍDA com sucesso, com negociacoes resgatadas"));
                                    acao = "(" + negociacao.intMoeda + " <img src='..Imagens/Negociacao.png' /> " + Helper.aplicarPlural("resgatada", negociacao.intMoeda) + ")";
                                }
                                else
                                {
                                    if (negociacao.bitCompradorTeveNegociacoes == true)
                                    {
                                        sbTemp.Append(Helper.iconeNegociacao("Comprador_concluido", "Negociação RECEBIDA por você CONCLUÍDA com sucesso"));
                                    }
                                    else if (negociacao.bitCompradorTeveNegociacoes == null)
                                    {
                                        sbTemp.Append(Helper.iconeNegociacao("Comprador_vermelho", "Negociação que seria RECEBIDA por você ainda NÃO confirmada"));
                                        acao = "(negociacao aconteceu ? <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.ID + "&action=Comprador_confirmado'>sim</a> | <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.ID + "&action=Comprador_negado'>não</a>)";
                                    }
                                    else
                                    {
                                        sbTemp.Append(Helper.iconeNegociacao("Comprador_concluido", "Negociação RECEBIDA por você CONCLUÍDA com sucesso"));
                                        acao = "(vendedor inseriu negociação confirmando)";
                                    }
                                }
                                break;
                            case "expirado_pendente":
                                if (perfilNestaNegociacao == "vendedor")
                                {
                                    sbTemp.Append(Helper.iconeNegociacao("Vendedor_vermelho", "Negociação que seria DADA por você ainda NÃO CONFIRMADA"));
                                    acao = "(negociação aconteceu ? <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.ID + "&action=vendedor_confirmado'>sim</a> | <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.ID + "&action=vendedor_negado'>não</a>)";
                                }
                                else
                                {
                                    if (negociacao.bitCompradorTeveNegociacoes == true)
                                    {
                                        sbTemp.Append(Helper.iconeNegociacao("Comprador_amarelo", "Negociação que foi RECEBIDA por você ainda NÃO CONFIRMADA pelo vendedor"));
                                        acao = "(aguardando confirmação)";
                                    }
                                    else
                                    {
                                        sbTemp.Append(Helper.iconeNegociacao("Comprador_vermelho", "Negociação que seria RECEBIDA por você ainda NÃO CONFIRMADA"));
                                        acao = "(negociação aconteceu ? <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.ID + "&action=Comprador_confirmado'>sim</a> | <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.ID + "&action=Comprador_negado'>não</a>)";
                                    }
                                }
                                break;
                            case "pedido":
                                if (perfilNestaNegociacao == "vendedor")
                                {
                                    sbTemp.Append(Helper.iconeNegociacao("Vendedor_vermelho", "Negociação a ser DADA por você, aguardando SUA aprovação"));
                                    acao = "(<a href='MinhaNegociacao.aspx?Codigo=" + negociacao.ID + "&action=vendedor_aceito'>aceitar</a> | <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.ID + "&action=vendedor_recusado'>recusar</a> | <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.ID + "'>ver mais detalhes</a>)";
                                }
                                else
                                {
                                    sbTemp.Append(Helper.iconeNegociacao("Comprador_amarelo", "Negociação a ser RECEBIDA por você, aguardando a aprovação DO vendedor"));
                                    acao = "(aguardando aprovação)";
                                }
                                break;
                            case "vendedor_aceito":
                                if (perfilNestaNegociacao == "vendedor")
                                {
                                    sbTemp.Append(Helper.iconeNegociacao("Vendedor_amarelo", "Negociação a ser DADA por você, aguardando a confirmação DO Comprador"));
                                    acao = "(aguardando confirmação)";
                                }
                                else
                                {
                                    sbTemp.Append(Helper.iconeNegociacao("Comprador_vermelho", "Negociação a ser RECEBIDA por você, aguardando SUA confirmação"));
                                    acao = "(<a href='MinhaNegociacao.aspx?Codigo=" + negociacao.ID + "&action=Comprador_aceito'>aceitar</a> | <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.ID + "&action=Comprador_recusado'>recusar</a> | <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.ID + "'>ver mais detalhes</a>)";
                                }
                                break;
                            case "Comprador_aceito":
                                if (perfilNestaNegociacao == "vendedor")
                                {
                                    sbTemp.Append(Helper.iconeNegociacao("Vendedor_verde", "Negociação a ser DADA por você"));
                                }
                                else
                                {
                                    sbTemp.Append(Helper.iconeNegociacao("Comprador_verde", "Negociação a ser RECEBIDA por você"));
                                }
                                if (titulo == "hoje" | titulo == "amanhã")
                                {
                                    acao = "(veja os <a href='MinhaNegociacao.aspx?Codigo=" + negociacao.ID + "'>detalhes</a> p/ se programar)";
                                }
                                break;
                        }
                        sbTemp.AppendLine("      </td>");
                        
                        sbTemp.Append("      <td class='profileTextUser menorUSER nowrap'>");
                        switch (tipodata)
                        {
                            case "diaemês":
                                sbTemp.Append(Helper.dataabreviada(dataehorarioNegociacao, false, "", "", false, true));
                                break;
                            case "diadasemana":
                                sbTemp.Append(Helper.diadasemanaExtenso(dataehorarioNegociacao.DayOfWeek + 1, true));
                                break;
                            case "diadasemanaehorário":
                                sbTemp.Append(Helper.diadasemanaExtenso(dataehorarioNegociacao.DayOfWeek + 1, true));
                                sbTemp.Append(" ");
                                sbTemp.Append(Helper.horario(dataehorarioNegociacao));
                                break;
                            case "horário":
                                sbTemp.Append(Helper.horario(dataehorarioNegociacao));
                                break;
                        }
                        sbTemp.AppendLine("      </td>");
                        
                        sbTemp.AppendLine("      <td>");
                        if (negociacao.strTitulo != null && perfilNestaNegociacao == "vendedor")
                        {
                            sbTemp.Append("<a href='MinhaNegociacao.aspx?Codigo=" + negociacao.ID + "' title='Visualizar esta negociação' class='link-ir-para-pagina'>\"" + negociacao.strTitulo + "\"</a>");
                        }
                        else
                        {
                            if (perfilNestaNegociacao == "vendedor")
                            {
                                sbTemp.Append("<a href='MinhaNegociacao.aspx?Codigo=" + negociacao.ID + "' title='Visualizar esta negociação' class='link-ir-para-pagina'>");
                                sbTemp.Append(Helper.limitar(Helper.nomeLocal(negociacao.strLocalPartida, Helper.UserName(), "local"), 30) + ",");
                                sbTemp.Append(Helper.limitar(Helper.nomeLocal(negociacao.strLocalChegada, Helper.UserName(), "local"), 30));
                                
                                if (negociacao.optTipo == "emprestimo")
                                {
                                    dynamic negociacaovolta = (from item in Helper.Locais() where item.CodigoID == negociacao.IDInversoitem select item);
                                    
                                    if (negociacao.strLocalPartida == negociacaovolta.strLocal)
                                    {
                                        sbTemp.Append(" <span class='menorUSER'>(ida e volta)</span>");
                                    }
                                    else
                                    {
                                        sbTemp.Append("," + Helper.limitar(Helper.nomeLocal(negociacaovolta.strLocal, Helper.UserName(), "local"), 30));
                                    }
                                }
                                sbTemp.Append("</a>");
                            }
                            else
                            {
                                sbTemp.Append("<a href='MinhaNegociacao.aspx?Codigo=" + negociacao.ID + "' title='Visualizar esta negociação' class='link-ir-para-pagina'>\"" + negociacao.strTituloComprador + "\"</a>");
                            }
                        }
                        
                        sbTemp.Append(" <h5>");
                        if (status == "Comprador_aceito")
                        {
                            sbTemp.Append("confirmada, ");
                        }
                        if (perfilNestaNegociacao == "vendedor")
                        {
                            sbTemp.Append("a " + Helper.nomeUsuario(negociacao.IDComprador).Replace("<a", "<a class='link-ir-para-pagina'"));
                        }
                        else
                        {
                            sbTemp.Append("de " + Helper.nomeUsuario(negociacao.IDVendedor).Replace("<a", "<a class='link-ir-para-pagina'"));
                        }
                        sbTemp.Append("</h5>");
                        
                        sbTemp.AppendLine(" <h5>" + acao + "</h5>");

                        sbTemp = sbTemp.Replace("<h5></h5>", "");

                        sbTemp.AppendLine("      </td>");
                        sbTemp.AppendLine("  </tr>");
                    }

                    sbTemp.AppendLine("</table>");
                }
            }

            return sbTemp.ToString();
        }
    }
}