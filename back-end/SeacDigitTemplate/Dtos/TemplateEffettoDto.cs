using SeacDigitTemplate.Model;

namespace SeacDigitTemplate.Dtos
{
    public class TemplateEffettoDto
    {
        public int IdTemplate { get; set; }
        public int IdRow { get; set; }
        public int IdDoc { get; set; }
        public int ContoDareId { get; set; }
        public int ContoAvereId { get; set; }
        public int VoceIVAId { get; set; }
        public decimal Indet { get; set; }
        public int TitoloInapplicabilita { get; set; }
        public int AliquotaIVAId { get; set; }
        public decimal Valore { get; set; }
        public decimal VariazioneF { get; set; }
        public int RifRow { get; set; }
    }
}
