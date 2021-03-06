﻿using SeacDigitTemplate.Model;

namespace SeacDigitTemplate.Dtos
{
    public class DocumentoDto
    {
        public int Id { get; set; }
        public decimal Totale { get; set; }
        public decimal RitenutaAcconto { get; set; }
        public DocumentoSospensioneEnum Sospeso { get; set; }
        public DocumentoTipoEnum Tipo { get; set; }
        public DocumentoCaratteristicaEnum Caratteristica { get; set; }
        public int CliforId { get; set; }
        public RegistroTipoEnum Registro { get; set; }
        public int RiferimentoDocumentoId { get; set; }
        public string Numero { get; set; }
        public string Descrizione { get; set; }
        public int Protocollo { get; set; }
        public int? TemplateGenerazioneEffettoDocumentoId { get; set; }
        public bool? isGenerated { get; set; }
    }
}
