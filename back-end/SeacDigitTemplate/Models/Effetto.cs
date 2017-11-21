using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeacDigitTemplate.Models;

namespace SeacDigitTemplate.Model
{
    public class Effetto
    {
        public List<EffettoDocumento> EffettoDocumentoList  { get; set; }
        public List<EffettoRiga>  EffettoRigaList{ get; set; }
    }
}
