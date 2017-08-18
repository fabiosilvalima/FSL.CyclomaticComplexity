using System;
using System.Collections.Generic;


namespace FSL.CyclomaticComplexity.Models.Novo.Services
{
    public interface IHelperService
    {
        string RetornarUserName();
        List<Negociacao> RetornarNegociacoesPorLocais();
        string FormatarNomeUsuario(string idUsuariosPassageiroNegociacoes);
        bool EhImpar(int numeroLinhaNegociacao);
        string RetornarIconeNegociacao(string v1, string v2);
        string AplicarPlural(string v, int intNegociacaos);
        string FormatarDataAbreviada(DateTime dataHorarioNegociacao, bool v1, object one, object two, bool v2, bool v3);
        string FormatarHorario(DateTime dataHorarioNegociacao);
        string FormatarDiaDaSemanaPorExtenso(DayOfWeek dayOfWeek, bool v);
        string FormatarNomeLocal(object strLocal, string userName, object localDesconhecido);
        string Limitar(object v1, int v2);
    }
}