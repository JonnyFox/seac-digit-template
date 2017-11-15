using SeacDigitTemplate.Models;

namespace SeacDigitTemplate.Model
{
    public class RigaDigitata
    {
        public int Id { get; set; }

        public int DocumentoId { get; set; }
        public Documento Documento { get; set; }

        public int? ContoDareId { get; set; }
        public Conto ContoDare { get; set; }

        public int? ContoAvereId { get; set; }
        public Conto ContoAvere { get; set; }

        public int? VoceIvaId { get; set; }
        public VoceIva VoceIva { get; set; }

        public int? AliquotaIvaId { get; set; }
        public AliquotaIva AliquotaIva { get; set; }

        public decimal? PercentualeAliquotaIva { get; set; }

        public TrattamentoEnum? Trattamento { get; set; }

        public int? TitoloInapplicabilitaId { get; set; }
        public TitoloInapplicabilita TitoloInapplicabilita { get; set; }

        public decimal? Imponibile { get; set; }
        public decimal? Iva { get; set; }
        public decimal? PercentualeIndetraibilita { get; set; }
        public decimal? PercentualeIndeducibilita { get; set; }
        public int? Settore { get; set; }
        public string Note { get; set; }
    }
}
