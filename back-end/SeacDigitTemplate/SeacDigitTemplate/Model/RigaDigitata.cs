using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Model
{
    public class RigaDigitata
    {
        public int Id;
        public int DocumentId;
        public int ContoDareId;
        public int ContoAvereId;
        public int VoceIVAId;
        public TrattamentoEnum Trattamento;
        public int TitoloInapplicabilita;
        public int AliquotaIVAId;
        public int Imponibile;
        public int IVA;
        public int PercentualeIndetraibilita;
        public int PercentualeIndeducibilita;
        public int Settore;
        public string Note;
    }
}
