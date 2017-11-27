using SeacDigitTemplate.Model;

namespace SeacDigitTemplate.Models
{
    public class TemplateRiga
    {
        public int Id { get; set; }

        public int ApplicazioneTemplateRigaId { get; set; }
        public ApplicazioneTemplateRiga ApplicazioneTemplateRiga { get; set; }

        public int? DocumentoId { get; set; }

        public string ContoDareId { get; set; }
        public string ContoAvereId { get; set; }
        public string VoceIvaId { get; set; }
        public string AliquotaIvaId { get; set; }
        public string PercentualeAliquotaIva { get; set; }
        public string Trattamento { get; set; }
        public string TitoloInapplicabilitaId { get; set; }
        public string Imponibile { get; set; }
        public string Iva { get; set; }
        public string PercentualeIndetraibilita { get; set; }
        public string PercentualeIndeducibilita { get; set; }
        public string Settore { get; set; }
        public string Note { get; set; }
    }
}
