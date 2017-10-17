using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Model
{
    public class SituazioneConto
    {
        public int ContoId { get; set; }
        public Conto Conto { get; set; }

        public int Valore { get; set; }
        public int Variazione { get; set; }
    }
}
