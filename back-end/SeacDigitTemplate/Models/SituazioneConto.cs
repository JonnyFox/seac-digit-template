using System;

namespace SeacDigitTemplate.Model
{
    public class SituazioneConto : IEquatable<SituazioneConto>
    {
        public int ContoId { get; set; }
        public Conto Conto { get; set; }

        public decimal Valore { get; set; }
        public decimal VariazioneFiscale { get; set; }

        public bool Equals(SituazioneConto other)
        {
            return ContoId.Equals(other.ContoId);
        }
    }
}
