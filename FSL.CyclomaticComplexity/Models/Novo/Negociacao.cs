using System;


namespace FSL.CyclomaticComplexity.Models.Novo
{
    public sealed class Negociacao
    {
        public DateTime HorarioNegociacao { get; set; }
        public DateTime DataNegociacao { get; set; }
        public string IdNegociacao { get; set; }
        public StatusNegociacao DbStatus { get; set; }
        public bool FlagVendedorTeveNegociacoes { get; set; }
        public bool? FlagCompradorTeveNegociacoes { get; set; }
        public string IdVendedor { get; set; }
        public int ValorMoeda { get; set; }
        public string Titulo { get; set; }
        public int Id { get; set; }
        public string IdComprador { get; set; }
        public string TituloComprador { get; set; }
        public string LocalPartida { get; set; }
        public string LocalChegada { get; set; }
        public string TipoOpcao { get; set; }
        public string CodigoId { get; set; }
        public string IdItemInverso { get; set; }
    }
}