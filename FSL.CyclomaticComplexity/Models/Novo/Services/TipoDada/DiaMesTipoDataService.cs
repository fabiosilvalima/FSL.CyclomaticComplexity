using System;


namespace FSL.CyclomaticComplexity.Models.Novo.Services
{
    public class DiaMesTipoDataService : ITipoDataService
    {
        private readonly IHelperService _helperService;

        public DiaMesTipoDataService(IHelperService helperService)
        {
            _helperService = helperService;
        }

        public string FormatarDataHoraNegociacao(DateTime dataHorarioNegociacao)
        {
            return _helperService.FormatarDataAbreviada(dataHorarioNegociacao, false, "", "", false, true);
        }
    }
}