using SeacDigitTemplate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Dtos
{
    public class SituazioneVoceIVA
    {
        public int VoceIVAId { get; set; }
        public int AliquotaIVAId { get; set; }
        public TrattamentoEnum Trattamento { get; set; }
        public int TitoloInapplicabilita { get; set; }
        public decimal Valore { get; set; }

    }
}
