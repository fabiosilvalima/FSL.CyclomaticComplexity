using System;
using System.Text;


namespace FSL.CyclomaticComplexity.Models.Novo.Services
{
    public class DiaDaSemanaHorarioTipoDataService : ITipoDataService
    {
        private readonly IHelperService _helperService;

        public DiaDaSemanaHorarioTipoDataService(IHelperService helperService)
        {
            _helperService = helperService;
        }

        public string FormatarDataHoraNegociacao(DateTime dataHorarioNegociacao)
        {
            var sbTemp = new StringBuilder();
            sbTemp.Append(_helperService.FormatarDiaDaSemanaPorExtenso(dataHorarioNegociacao.DayOfWeek + 1, true));
            sbTemp.Append(" ");
            sbTemp.Append(_helperService.FormatarHorario(dataHorarioNegociacao));

            return sbTemp.ToString();
        }
    }
}