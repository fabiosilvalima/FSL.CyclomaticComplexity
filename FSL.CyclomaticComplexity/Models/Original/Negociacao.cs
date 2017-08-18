using System;

namespace FSL.CyclomaticComplexity.Models.Original
{
    public class Negociacao
    {
        public DateTime datHorarioNegociacao { get; set; }
        public DateTime datNegociacao { get; set; }
        public string IDNegociacao { get; set; }
        public string dbStatus { get; set; }
        public bool bitVendedorTeveNegociacoes { get; set; }
        public bool? bitCompradorTeveNegociacoes { get; set; }
        public string IDVendedor { get; set; }
        public int intMoeda { get; set; }
        public string strTitulo { get; set; }
        public int ID { get; set; }
        public string IDComprador { get; set; }
        public string strTituloComprador { get; set; }
        public string strLocalPartida { get; set; }
        public string strLocalChegada { get; set; }
        public string optTipo { get; set; }
        public string CodigoID { get; set; }
        public string IDInversoitem { get; set; }
    }
}