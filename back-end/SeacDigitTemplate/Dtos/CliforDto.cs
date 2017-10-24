using SeacDigitTemplate.Model;

namespace SeacDigitTemplate.Dtos
{
    public class CliforDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public CliforSoggettoEnum Soggetto { get; set; }
        public CliforIstituzionaleEnum Istituzionale { get; set; }
    }
}