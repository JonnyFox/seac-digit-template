namespace SeacDigitTemplate.Model
{
    public class SituazioneConto
    {
        public int ContoId { get; set; }
        public Conto Conto { get; set; }

        public decimal Valore { get; set; }
        public decimal Variazione { get; set; }
    }
}
