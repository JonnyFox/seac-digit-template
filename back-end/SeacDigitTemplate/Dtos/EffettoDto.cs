namespace SeacDigitTemplate.Dtos
{
    public class EffettoDto
    {
        public int Id { get; set; }
        public int IdRow { get; set; }
        public int IdDoc { get; set; }
        public int? ContoDareId { get; set; }
        public int? ContoAvereId { get; set; }
        public int? VoceIVAId { get; set; }
        public string TitoloInapplicabilita { get; set; }
        public int? AliquotaIVAId { get; set; }
        public decimal Valore { get; set; }
        public decimal VariazioneF { get; set; }
        public decimal Imponibile { get; set; }
        public decimal Iva { get; set; }
        public int? RiferimentoEffetto { get; set; }
        public string DataOperazione { get; set; }
        public int TemplateGenerazioneEffetto { get; set; }
    }
}
