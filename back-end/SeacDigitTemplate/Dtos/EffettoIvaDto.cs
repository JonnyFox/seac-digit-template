using SeacDigitTemplate.Model;
namespace SeacDigitTemplate.Dtos
{
    public class EffettoIvaDto
    {

        public int Id { get; set; }
        public int RigaDigitataId { get; set; }
        public int IdDoc { get; set; }
        public int? VoceIvaId { get; set; }
        public TrattamentoEnum? Trattamento { get; set; }

        public int? TitoloInapplicabilitaId { get; set; }
        public int? AliquotaIvaId { get; set; }
        public decimal Imponibile { get; set; }
        public decimal Iva { get; set; }
        public int TemplateGenerazioneEffetto { get; set; }
    }
}
