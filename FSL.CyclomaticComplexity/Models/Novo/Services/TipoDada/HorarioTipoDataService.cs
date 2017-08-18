using System;


namespace FSL.CyclomaticComplexity.Models.Novo.Services
{
    public class HorarioTipoDataService : ITipoDataService
    {
        private readonly IHelperService _helperService;

        public HorarioTipoDataService(IHelperService helperService)
        {
            _helperService = helperService;
        }

        public string FormatarDataHoraNegociacao(DateTime dataHorarioNegociacao)
        {
            return _helperService.FormatarHorario(dataHorarioNegociacao);
        }
    }
}