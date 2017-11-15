using SeacDigitTemplate.Model;

namespace SeacDigitTemplate.Models
{
    public class TemplateEffetto
    {
        public int Id { get; set; }

        public int ApplicazioneTemplateEffettoId { get; set; }
        public ApplicazioneTemplateEffetto ApplicazioneTemplateEffetto { get; set; }

        public string ContoDareId { get; set; }
        public string ContoAvereId { get; set; }
        public string VoceIvaId { get; set; }
        public string Trattamento { get; set; }
        public string TitoloInapplicabilitaId { get; set; }
        public string AliquotaIvaId { get; set; }
        public string Valore { get; set; }
        public string VariazioneFiscale { get; set; }
        public string Imponibile { get; set; }
        public string Iva { get; set; }
        public string RifRow { get; set; }
        public string DataOperazione { get; set; }
    }
}
