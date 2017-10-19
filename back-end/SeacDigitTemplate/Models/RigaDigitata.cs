using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Model
{
    public class RigaDigitata
    {
        public int Id { get; set; }

        public int DocumentoId { get; set; }
        public Documento Documento { get; set; }

        public int ContoDareId { get; set; }
        public Conto ContoDare { get; set; }

        public int ContoAvereId { get; set; }
        public Conto ContoAvere { get; set; }

        public int VoceIVAId { get; set; }
        public VoceIva VoceIVA { get; set; }

        public int AliquotaIVAId { get; set; }
        public AliquotaIva AliquotaIVA { get; set; }

        public TrattamentoEnum Trattamento { get; set; }
        public int TitoloInapplicabilita { get; set; }
        public decimal Imponibile { get; set; }
        public decimal IVA { get; set; }
        public decimal PercentualeIndetraibilita { get; set; }
        public decimal PercentualeIndeducibilita { get; set; }
        public int Settore { get; set; }
        public string Note { get; set; }
    }
}
