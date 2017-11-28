using SeacDigitTemplate.Models;
using System.Collections.Generic;

namespace SeacDigitTemplate.Model
{
    public class Documento
    {
        public int Id { get; set; }
        public decimal Totale { get; set; }

        public decimal? RitenutaAcconto { get; set; }

        public DocumentoSospensioneEnum Sospeso { get; set; }
        public DocumentoTipoEnum Tipo { get; set; }
        public DocumentoCaratteristicaEnum Caratteristica { get; set; }
        public int CliforId { get; set; }
        public Clifor Clifor { get; set; }

        public RegistroTipoEnum Registro { get; set; }
        public int RiferimentoDocumentoId { get; set; }

        public string Numero { get; set; }
        public int Protocollo { get; set; }

        public int? TemplateGenerazioneEffettoDocumentoId { get; set; }
        public TemplateDocumento TemplateGenerazioneEffettoDocumento { get;set; }
        public List<RigaDigitata> rigaDigitataList { get; set; }
    }
}
