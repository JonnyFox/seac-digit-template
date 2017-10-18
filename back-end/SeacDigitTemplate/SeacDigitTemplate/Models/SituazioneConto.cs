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

        public decimal Valore { get; set; }
        public decimal Variazione { get; set; }
    }
}
