using SeacDigitTemplate.Model;

namespace SeacDigitTemplate.Models
{
    public class AppTemplate
    {
        public int IdTemplate { get; set; }
        public int IdEff { get; set; }
        public int IdDoc { get; set; }
        public int ContoDareId { get; set; }
        public Conto ContoDare { get; set; }

        public int ContoAvereId { get; set; }
        public Conto ContoAvere { get; set; }

        public int VoceIVAId { get; set; }
        public VoceIva VoceIVA { get; set; }

        public decimal Indet { get; set; }
        public int TitoloInapplicabilita { get; set; }

        public int AliquotaIVAId { get; set; }
        public AliquotaIva AliquotaIVA { get; set; }
        public decimal Imponibile { get; set; }
        public decimal IVA { get; set; }
        public decimal PercentualeIndetraibilita { get; set; }
    }
}
