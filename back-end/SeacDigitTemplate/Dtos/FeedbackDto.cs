using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Model
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public int IdDoc { get; set; }
        public string Descrizione { get; set; }
        public string DescrizioneDoc { get; set; }
        public string Effetto { get; set; }
    }
}
