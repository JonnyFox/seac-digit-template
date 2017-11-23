using SeacDigitTemplate.Models;

namespace SeacDigitTemplate.Model
{
    public class EffettoRiga
    {
        public int Id { get; set; }
        public int RigaDigitataId { get; set; }
        public int DocumentoId { get; set; }
        public int EffettoDocumentoId { get; set; }
        public int? ContoDareId { get; set; }
        public Conto ContoDare { get; set; }

        public int? ContoAvereId { get; set; }
        public Conto ContoAvere { get; set; }

        public int? VoceIvaId { get; set; }
        public VoceIva VoceIva { get; set; }

        public TrattamentoEnum? Trattamento { get; set; }

        public int? TitoloInapplicabilitaId { get; set; }
        public TitoloInapplicabilita TitoloInapplicabilita { get; set; }

        public int? AliquotaIvaId { get; set; }
        public AliquotaIva AliquotaIva { get; set; }

        public decimal Valore { get; set; }
        public decimal VariazioneFiscale { get; set; }
        public decimal Imponibile { get; set; }
        public decimal Iva { get; set; }

        public int? RiferimentoEffettoRigaId { get; set; }
        public EffettoRiga RiferimentoEffettoRiga { get; set; }

        public string DataOperazione { get; set; }

        public int TemplateGenerazioneEffettoRiga { get; set; }
        public TemplateEffettoRiga TemplateEffetto { get; set; }
    }
}
