using SeacDigitTemplate.Model;
using SeacDigitTemplate.Models;

namespace SeacDigitTemplate.Dtos
{
    public class RigaDigitataDto
    {
        public int Id { get; set; }

        public int DocumentoId { get; set; }
        public int ContoDareId { get; set; }
        public int ContoAvereId { get; set; }
        public int? VoceIvaId { get; set; }
        public int? AliquotaIvaId { get; set; }
        public TrattamentoEnum? Trattamento { get; set; }
        public int? TitoloInapplicabilitaId { get; set; }
        public decimal? Imponibile { get; set; }
        public decimal? Iva { get; set; }
        public decimal PercentualeIndetraibilita { get; set; }
        public decimal PercentualeIndeducibilita { get; set; }
        public int? Settore { get; set; }
        public string Note { get; set; }
    }
}
