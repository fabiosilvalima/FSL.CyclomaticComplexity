
namespace FSL.CyclomaticComplexity.Models.Novo
{
    public enum StatusNegociacao
    {
        None = 0,
        ExpiradoPendente = 1,
        ExpiradoFalho = 2,
        ExpiradoSemNegociacao = 3,
        ExpiradoCancelado = 4,
        ExpiradoConcluido = 5,
        Pedido = 6,
        VendedorAceito = 7,
        CompradorAceito = 8,
        Recusado = 9
    }

    public enum PerfilNegociacao
    {
        Comprador = 1,
        Vendedor = 2
    }

    public enum TipoData
    {
        DiaMes = 1,
        DiaDaSemana = 2,
        DiaDaSemanaHorario = 3,
        Horario = 4
    }
}