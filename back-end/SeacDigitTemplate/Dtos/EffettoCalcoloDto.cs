using System.Collections.Generic;

namespace SeacDigitTemplate.Dtos
{
    public class EffettoCalcoloDto
    {
        public List<EffettoContoDto> EffettoContos { get; set; }
        public List<EffettoIvaDto> EffettoIvas { get; set; }
    }
}
