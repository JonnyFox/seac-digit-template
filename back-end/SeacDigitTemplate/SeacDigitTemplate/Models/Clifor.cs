namespace SeacDigitTemplate.Model
{
    public class Clifor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public CliforSoggettoEnum Soggetto { get; set; }
        public CliforIstituzionaleEnum Istituzionale { get; set; }
    }
}
