using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Model
{
    public class Documento
    {
        public int Id { get; set; }
        public decimal Totale { get; set; }
        public decimal RitenutaAcconto { get; set; }
        public DocumentoSospensioneEnum Sospeso { get; set; }
        public DocumentoTipoEnum Tipo { get; set; }
        public DocumentoCaratteristicaEnum Caratteristica { get; set; }
        public int CliforId { get; set; }
        public Clifor Clifor { get; set; }

        public RegistroTipoEnum Registro { get; set; }
        public int RiferimentoDocumentoId { get; set; }
    }
}
