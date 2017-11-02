using SeacDigitTemplate.Model;

namespace SeacDigitTemplate.Dtos
{
    public class SituazioneVoceIvaDto
    {
        public int VoceIvaId { get; set; }
        public int? AliquotaIvaId { get; set; }
        public TrattamentoEnum Trattamento { get; set; }
        public int? TitoloInapplicabilita { get; set; }
        public decimal Valore { get; set; }

    }
}
