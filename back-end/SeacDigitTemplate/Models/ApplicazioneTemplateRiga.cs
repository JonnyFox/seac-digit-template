using System.Collections.Generic;

namespace SeacDigitTemplate.Models
{
    public class ApplicazioneTemplateRiga
    {
        public int Id { get; set; }
        public string ContoDare { get; set; }
        public string ContoAvere { get; set; }
        public string VoceIva { get; set; }
        public string Trattamento { get; set; }
        public string TitoloInapplicabilita { get; set; }
        public string AliquotaIva { get; set; }
        public string Imponibile { get; set; }
        public string Iva { get; set; }
        public string PercentualeIndetraibilita { get; set; }
        public string PercentualeIndeducibilita { get; set; }
        public string Settore { get; set; }
        public string Note { get; set; }
        public string RitenutaAcconto { get; set; }
        public string Sospeso { get; set; }
        public string Tipo { get; set; }
        public string Caratteristica { get; set; }
        public string Registro { get; set; }
        public string RitenutaAccontoEffetto { get; set; }
        public string SospesoEffetto { get; set; }
        public string TipoEffetto { get; set; }
        public string CaratteristicaEffetto { get; set; }
        public string RegistroEffetto { get; set; }

        public ICollection<TemplateRiga> TemplateEffetto { get; set; }
    }
}
