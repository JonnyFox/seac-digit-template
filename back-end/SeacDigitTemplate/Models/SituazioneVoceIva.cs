using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Model
{
    public class SituazioneVoceIva
    {
        public int VoceIvaId { get; set; }
        public VoceIva VoceIva { get; set; }

        public int AliquotaIvaId { get; set; }
        public AliquotaIva AliquotaIva { get; set; }

        public TrattamentoEnum Trattamento { get; set; }
        public int TitoloInapplicabilita { get; set; }
        public decimal Valore { get; set; }

    }
}
