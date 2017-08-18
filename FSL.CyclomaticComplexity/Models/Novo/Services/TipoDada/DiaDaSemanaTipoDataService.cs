using System;


namespace FSL.CyclomaticComplexity.Models.Novo.Services
{
    public class DiaDaSemanaTipoDataService : ITipoDataService
    {
        private readonly IHelperService _helperService;

        public DiaDaSemanaTipoDataService(IHelperService helperService)
        {
            _helperService = helperService;
        }

        public string FormatarDataHoraNegociacao(DateTime dataHorarioNegociacao)
        {
            return _helperService.FormatarDiaDaSemanaPorExtenso(dataHorarioNegociacao.DayOfWeek + 1, true);
        }
    }
}