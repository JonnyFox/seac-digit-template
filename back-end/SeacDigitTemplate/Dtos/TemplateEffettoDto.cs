namespace SeacDigitTemplate.Dtos
{
    public class TemplateEffettoDto
    {
        public int Id { get; set; }
        public int? ContoDareId { get; set; }
        public int? ContoAvereId { get; set; }
        public int? VoceIVAId { get; set; }
        public decimal Trattamento { get; set; }
        public int TitoloInapplicabilita { get; set; }
        public int? AliquotaIVAId { get; set; }
        public decimal Valore { get; set; }
        public decimal VariazioneFiscale { get; set; }
        public decimal Imponibile { get; set; }
        public decimal Iva { get; set; }
        public int RifRow { get; set; }
        public string DataOperazione { get; set; }
    }
}
