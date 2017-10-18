using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Model
{
    public class Effetto
    {
        public int Id { get; set; }
        public int DocumentoId { get; set; }
        public int RigaDigitataId { get; set; }
        public int ContoDareId { get; set; }
        public int ContoAvereId { get; set; }
        public int VoceIVAId { get; set; }
        public TrattamentoEnum Trattamento { get; set; }
        public int TitoloInapplicabilita { get; set; }
        public int AliquotaIVAId { get; set; }
        public int Imponibile { get; set; }
        public int Valore { get; set; }
        public int Variazione { get; set; }
        public int RiferimentoEffettoId { get; set; }
    }
}
