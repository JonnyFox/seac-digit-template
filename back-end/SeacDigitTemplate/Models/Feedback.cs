namespace SeacDigitTemplate.Model
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Descrizione { get; set; }
        public string Effetto { get; set; }
        public int DocumentoId { get; set; }
        public Documento Documento { get; set; }
    }
}
