using SeacDigitTemplate.Model;

namespace SeacDigitTemplate.Dtos
{
    public class EffettoDocumentoDto
    {
        public int Id { get; set; }
        public decimal Totale { get; set; }
        public decimal? RitenutaAcconto { get; set; }
        public DocumentoSospensioneEnum Sospeso { get; set; }
        public DocumentoTipoEnum tipo { get; set; }
        public DocumentoCaratteristicaEnum caratteristica { get; set; }
        public int? CliforId { get; set; }
        public RegistroTipoEnum Registro { get; set; }
        public int? RiferimentoDocumentoId { get; set; }
        public string Numero { get; set; }
        public int? Protocollo { get; set; }
    }
}