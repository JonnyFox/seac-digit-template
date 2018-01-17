namespace SeacDigitTemplate.Model
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public string Descrizione { get; set; }
        public string Effetto { get; set; }
        public int DocumentoId { get; set; }
        public string DocumentoDescrizione { get; set; }
    }
}
