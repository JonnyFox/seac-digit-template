using SeacDigitTemplate.Model;

namespace SeacDigitTemplate.Models
{
    public class ApplicazioneTemplateEffetto
    {
        public int Id { get; set; }
        public string ContoDare { get; set; }
        public string ContoAvere { get; set; }
        public string VoceIva{ get; set; }
        public string Trattamento { get; set; }
        public string TitoloInapplicabilita { get; set; }
        public string AliquotaIva { get; set; }
        public string Imponibile { get; set; }
        public string Iva { get; set; }
        public string PercentualeIndetraibilita { get; set; }
        public string PercentualeIndeducibilita { get; set; }
        public string Settore { get; set; }
        public string Note { get; set; }
    }
}
