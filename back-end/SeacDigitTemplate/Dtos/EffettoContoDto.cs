namespace SeacDigitTemplate.Dtos
{
    public class EffettoContoDto
    {
        public int Id { get; set; }
        public int RigaDigitataId { get; set; }
        public int IdDoc { get; set; }
        public int? ContoDareId { get; set; }
        public int? ContoAvereId { get; set; }
        public decimal Valore { get; set; }
        public decimal VariazioneF { get; set; }
        public int TemplateGenerazioneEffetto { get; set; }
    }
}
