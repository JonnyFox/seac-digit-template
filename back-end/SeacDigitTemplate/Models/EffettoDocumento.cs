using SeacDigitTemplate.Models;
using System.Collections.Generic;

namespace SeacDigitTemplate.Model
{
    public class EffettoDocumento
    {
        public int Id { get; set; }

        public int DocumentoId { get; set; }
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

        public List<RigaDigitata> rigaDigitataList { get; set; }

        public int? RiferimentoEffettoId { get; set; }
        public EffettoDocumento RiferimentoEffetto { get; set; }

        public string DataOperazione { get; set; }

        public int TemplateGenerazioneEffetto { get; set; }
        public TemplateEffetto TemplateEffetto { get; set; }
    }
}
