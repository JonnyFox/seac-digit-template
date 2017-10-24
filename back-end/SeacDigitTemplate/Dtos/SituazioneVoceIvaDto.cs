using SeacDigitTemplate.Model;

namespace SeacDigitTemplate.Dtos
{
    public class SituazioneVoceIvaDto
    {
        public int VoceIVAId { get; set; }
        public int AliquotaIVAId { get; set; }
        public TrattamentoEnum Trattamento { get; set; }
        public int TitoloInapplicabilita { get; set; }
        public decimal Valore { get; set; }

    }
}
