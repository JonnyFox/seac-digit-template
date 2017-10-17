using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Model
{
    public class Documento
    {
        public int Id;
        public int Totale;
        public int RitenutaAcconto;
        public DocumentoSospensioneEnum Sospeso;
        public DocumentoTipoEnum Tipo;
        public DocumentoCaratteristicaEnum Caratteristica;
        public int CliforId;
        public RegistroTipoEnum Registro;
        public int RiferimentoDocumentoId;
    }
}
