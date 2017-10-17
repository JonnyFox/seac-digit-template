using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Model
{
    public class RigaDigitata
    {
        public int Id { get; set; }

        public int DocumentId { get; set; }
        public Documento Documento { get; set; }

        public int ContoDareId { get; set; }
        public Conto ContoDare { get; set; }

        public int ContoAvereId { get; set; }
        public Conto ContoAvere { get; set; }

        public int VoceIVAId { get; set; }
        public VoceIVA VoceIVA { get; set; }

        public int AliquotaIVAId { get; set; }
        public AliquotaIVA AliquotaIVA { get; set; }

        public TrattamentoEnum Trattamento { get; set; }
        public int TitoloInapplicabilita { get; set; }
        public int Imponibile { get; set; }
        public int IVA { get; set; }
        public int PercentualeIndetraibilita { get; set; }
        public int PercentualeIndeducibilita { get; set; }
        public int Settore { get; set; }
        public string Note { get; set; }
    }
}
