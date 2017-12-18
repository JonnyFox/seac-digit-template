using SeacDigitTemplate.Model;

namespace SeacDigitTemplate.Dtos
{
    public class SituazioneVoceIvaDto
    {
        public int VoceIvaId { get; set; }
        public int? AliquotaIvaId { get; set; }
        public TrattamentoEnum? Trattamento { get; set; }
        public int? TitoloInapplicabilitaId { get; set; }
        public decimal Imponibile { get; set; }
        public decimal Iva { get; set; }
        public decimal Sospeso { get; set; }


    }
}
