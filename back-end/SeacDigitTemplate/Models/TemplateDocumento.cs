using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeacDigitTemplate.Model;

namespace SeacDigitTemplate.Models
{
    public class TemplateDocumento
    {
        public int Id { get; set; }

        public int ApplicazioneTemplateDocumentoId { get; set; }
        public ApplicazioneTemplateDocumento ApplicazioneTemplateDocumento { get; set; }

        //public int IdDocumento { get; set; }
        public string Totale { get; set; }
        public string RitenutaAcconto { get; set; }
        public string Sospeso { get; set; }
        public string Tipo { get; set; }
        public string Caratteristica { get; set; }
        public string CliforId { get; set; }
        public string Registro { get; set; }
        public string RiferimentoDocumentoId { get; set; }
        public string Numero { get; set; }
        public string Protocollo { get; set; }
        //public List<RigaDigitata> rigaDigitataList { get; set; }
    }
}
