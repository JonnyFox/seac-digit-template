using SeacDigitTemplate.Model;

namespace SeacDigitTemplate.Models
{
    public class TemplateEffetto
    {
        public int Id { get; set; }
        public int IdRow { get; set; }
        public int IdDoc { get; set; }
        public int? ContoDareId { get; set; }
        public Conto ContoDare { get; set; }

        public int? ContoAvereId { get; set; }
        public Conto ContoAvere { get; set; }

        public int? VoceIVAId { get; set; }
        public VoceIva VoceIVA { get; set; }

        public decimal Indet { get; set; }
        public int TitoloInapplicabilita { get; set; }

        public int? AliquotaIVAId { get; set; }
        public AliquotaIva AliquotaIVA { get; set; }
        public decimal Valore { get; set; }
        public decimal VariazioneF { get; set; }
        public int RifRow { get; set; }
    }
}
