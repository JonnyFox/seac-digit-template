﻿using SeacDigitTemplate.Model;
using SeacDigitTemplate.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SeacDigitTemplate.Data
{
    public class DbInitializer
    {
        public static void Initialize(SeacDigitTemplateContext context)
        {
            context.Database.EnsureCreated();

            var trans = context.Database.BeginTransaction();

            if (!context.Clifors.Any())
            {
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Contos ON");

                var contos = new Conto[]
                {
                  new Conto{ Id = 1, Nome ="Merce"},
                  new Conto{ Id = 2, Nome ="Merce2"},
                  new Conto{ Id = 3, Nome ="Valori Bollati"},
                  new Conto{ Id = 4, Nome ="Iva cr"},
                  new Conto{ Id = 5, Nome ="Salari/Stipendi"},
                  new Conto{ Id = 6, Nome ="Anticipo IVS"},
                  new Conto{ Id = 7, Nome ="Transitorio"},
                  new Conto{ Id = 8, Nome ="Compenso"},
                  new Conto{ Id = 9, Nome ="Contributo Previdenziale"},
                  new Conto{ Id = 10, Nome ="Fornitore"},
                  new Conto{ Id = 11, Nome ="Spese"},
                  new Conto{ Id = 12, Nome ="Iva cr(s)"},
                  new Conto{ Id = 13, Nome ="Iva Transitorio"},
                  new Conto{ Id = 14, Nome ="Debito INPS"},
                  new Conto{ Id = 15, Nome ="Erario c.ritenute"},
                  new Conto{ Id = 16, Nome ="Debito ass.sanit"},
                  new Conto{ Id = 17, Nome ="Ente bilaterale"},
                  new Conto{ Id = 18, Nome ="Deb vs dipendenti"},
                  new Conto{ Id = 19, Nome = "RitenutaAcconto"},
                  new Conto{ Id = 20, Nome = "Consulenza"},
                  new Conto{ Id = 21, Nome = "Banca"}

                };
                foreach (Conto c in contos)
                {
                    context.Contos.Add(c);
                }

                context.SaveChanges();

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Contos OFF");


                trans.Commit();

                var clifors = new Clifor[]
                {
                  new Clifor{Nome ="Cli For 1", Soggetto = CliforSoggettoEnum.ExtraUE, Istituzionale = CliforIstituzionaleEnum.Istituzionale},
                  new Clifor{Nome ="Cli For 2", Soggetto = CliforSoggettoEnum.Nazionale, Istituzionale = CliforIstituzionaleEnum.IstituzionaleCommerciale},
                  new Clifor{Nome ="Cli For 3", Soggetto = CliforSoggettoEnum.UE, Istituzionale = CliforIstituzionaleEnum.Commerciale}
                };

                foreach (Clifor c in clifors)
                {
                    context.Clifors.Add(c);
                }


                var aliquotas = new AliquotaIva[]
                {
                new AliquotaIva{Percentuale = 4m},
                new AliquotaIva{Percentuale = 10m},
                new AliquotaIva{Percentuale = 22m}
                };

                foreach (AliquotaIva a in aliquotas)
                {
                    context.AliquotaIvas.Add(a);
                }


                var voceivas = new VoceIva[]
                {
                 new VoceIva{Nome = "Merce"},
                 new VoceIva{Nome = "Spese"},
                 new VoceIva{Nome = "Consulenza"}
                };

                foreach (VoceIva v in voceivas)
                {
                    context.VoceIvas.Add(v);

                }

                var documentos = new Documento[]
                {
                new Documento
                {
                    Totale = 0.0m,
                    RitenutaAcconto = 1.0m,
                    Sospeso = DocumentoSospensioneEnum.None,
                    Tipo = DocumentoTipoEnum.Fattura,
                    Caratteristica = DocumentoCaratteristicaEnum.Normale,
                    Clifor = clifors[0],
                    Registro = RegistroTipoEnum.Emesse,
                    Numero = "1",
                    Protocollo = 1,
                    isGenerated = false,
                    Descrizione ="Caso base"

                },
                new Documento{
                    Totale = 45.0m,
                    RitenutaAcconto = 0.0m,
                    Sospeso = DocumentoSospensioneEnum.None,
                    Tipo = DocumentoTipoEnum.Fattura,
                    Caratteristica = DocumentoCaratteristicaEnum.Normale,
                    Clifor = clifors[1],
                    Registro = RegistroTipoEnum.Emesse,
                    Numero = "2",
                    Protocollo = 5,
                    isGenerated = false,
                    Descrizione ="Caso indetraibilità"
                    },
                new Documento{
                    Totale = 45.0m,
                    RitenutaAcconto = 0.0m,
                    Sospeso = DocumentoSospensioneEnum.None,
                    Tipo = DocumentoTipoEnum.NotaAccredito,
                    Caratteristica = DocumentoCaratteristicaEnum.Normale,
                    Clifor = clifors[1],
                    Registro = RegistroTipoEnum.Acquisti,
                    Numero = "3",
                    Protocollo = 5,
                    isGenerated = false,
                    Descrizione ="Caso Indetraibilità e Indeducibilità"
                    },
                new Documento{
                    Totale = 30.0m,
                    RitenutaAcconto = 0.0m,
                    Sospeso = DocumentoSospensioneEnum.None,
                    Tipo = DocumentoTipoEnum.Fattura,
                    Caratteristica = DocumentoCaratteristicaEnum.Normale,
                    Clifor = clifors[1],
                    Registro = RegistroTipoEnum.Acquisti,
                    Numero = "4",
                    Protocollo = 5,
                    isGenerated = false,
                    Descrizione ="Caso base esteso"
                    },
                new Documento{
                    Totale = 60.0m,
                    RitenutaAcconto = 204.0m,
                    Sospeso = DocumentoSospensioneEnum.None,
                    Tipo = DocumentoTipoEnum.Fattura,
                    Caratteristica = DocumentoCaratteristicaEnum.Normale,
                    Clifor = clifors[1],
                    Registro = RegistroTipoEnum.Finanziari,
                    Numero = "5",
                    Protocollo = 5,
                    isGenerated = false,
                    Descrizione ="Caso base + Ritenuta acconto"
                    },
                new Documento{
                    Totale = 320.0m,
                    RitenutaAcconto = 0.0m,
                    Sospeso = DocumentoSospensioneEnum.None,
                    Tipo = DocumentoTipoEnum.BollaDoganale,
                    Caratteristica = DocumentoCaratteristicaEnum.Normale,
                    Clifor = clifors[0],
                    Registro = RegistroTipoEnum.Acquisti,
                    Numero = "6",
                    Protocollo = 5,
                    isGenerated = false,
                    Descrizione ="Caso Bolla doganale"
                    },
                new Documento{
                    Totale = 1000.0m,
                    RitenutaAcconto = 0.0m,
                    Sospeso = DocumentoSospensioneEnum.None,
                    Tipo = DocumentoTipoEnum.Fattura,
                    Caratteristica = DocumentoCaratteristicaEnum.FatturaUE,
                    Clifor = clifors[0],
                    Registro = RegistroTipoEnum.Acquisti,
                    Numero = "7",
                    Protocollo = 5,
                    isGenerated = false,
                    Descrizione ="Caso Ue/Reverse"
                    },
                new Documento{
                    Totale = 1040.4m,
                    RitenutaAcconto = 204.0m,
                    Sospeso = DocumentoSospensioneEnum.None,
                    Tipo = DocumentoTipoEnum.Fattura,
                    Caratteristica = DocumentoCaratteristicaEnum.Normale,
                    Clifor = clifors[0],
                    Registro = RegistroTipoEnum.Acquisti,
                    Numero = "8",
                    Protocollo = 5,
                    isGenerated = false,
                    Descrizione ="Caso Sospensione"
                    }

                };

                foreach (var d in documentos)
                {
                    context.Documentos.Add(d);
                }

                var titoloInapplicabilitas = new TitoloInapplicabilita[]
                 {
                 new TitoloInapplicabilita{Nome = "Art 15"},
                 new TitoloInapplicabilita{Nome = "Art 16"},
                 new TitoloInapplicabilita{Nome = "Art 17"}
                 };

                foreach (TitoloInapplicabilita t in titoloInapplicabilitas)
                {
                    context.TitoloInapplicabilitas.Add(t);
                }

                var rigaDigitatas = new RigaDigitata[]
                {
                //Documento 1
                new RigaDigitata{
                    Documento = documentos[0],
                    ContoDare = contos[0],
                    ContoAvere = contos[9],
                    VoceIva = voceivas[0],
                    AliquotaIva = aliquotas[2],
                    PercentualeAliquotaIva = aliquotas[2].Percentuale,
                    Trattamento = TrattamentoEnum.Detraibile,
                    Imponibile = 1000.0m,
                    Iva = 220.0m
                },
                new RigaDigitata{
                    Documento = documentos[0],
                    ContoDare = contos[1],
                    ContoAvere = contos[9],
                    VoceIva = voceivas[0],
                    AliquotaIva = aliquotas[1],
                    PercentualeAliquotaIva = aliquotas[1].Percentuale,
                    Trattamento = TrattamentoEnum.Detraibile,
                    Imponibile = 1000.0m,
                    Iva = 100
                },
                new RigaDigitata{
                    Documento = documentos[0],
                    ContoDare = contos[2],
                    ContoAvere = contos[9],
                    VoceIva = voceivas[0],
                    Trattamento = TrattamentoEnum.Esente,
                    TitoloInapplicabilita = titoloInapplicabilitas[0],
                    Imponibile = 2.0m
                },
                //Documento 2
                new RigaDigitata{
                    Documento = documentos[1],
                    ContoDare = contos[0],
                    ContoAvere = contos[9],
                    VoceIva = voceivas[0],
                    AliquotaIva = aliquotas[2],
                    Trattamento = TrattamentoEnum.IndetraibileOggettivo,
                    PercentualeAliquotaIva = aliquotas[2].Percentuale,
                    Imponibile = 1000.0m,
                    Iva = 220,
                    PercentualeIndetraibilita = 40m,
                },
                //Documento 3
                new RigaDigitata{
                    Documento = documentos[2],
                    ContoDare = contos[0],
                    ContoAvere = contos[9],
                    VoceIva = voceivas[0],
                    Trattamento = TrattamentoEnum.IndetraibileOggettivo,
                    AliquotaIva = aliquotas[2],
                    PercentualeAliquotaIva = aliquotas[2].Percentuale,
                    Imponibile = 1000.0m,
                    Iva = 220,
                    PercentualeIndetraibilita = 40m,
                    PercentualeIndeducibilita = 20m
                },
                //Documento 4
                new RigaDigitata{
                    Documento = documentos[3],
                    ContoDare = contos[4],
                    ContoAvere = contos[6],
                    Imponibile = 3385.51m,
                },
                new RigaDigitata{
                    Documento = documentos[3],
                    ContoDare = contos[5],
                    ContoAvere = contos[6],
                    Imponibile = 17.07m,
                },
                new RigaDigitata{
                    Documento = documentos[3],
                    ContoDare = contos[6],
                    ContoAvere = contos[13],
                    Imponibile = 330.91m,
                },
                new RigaDigitata{
                    Documento = documentos[3],
                    ContoDare = contos[6],
                    ContoAvere = contos[14],
                    Imponibile = 489.79m,
                },
                new RigaDigitata{
                    Documento = documentos[3],
                    ContoDare = contos[6],
                    ContoAvere = contos[15],
                    Imponibile = 4m,
                },
                new RigaDigitata{
                    Documento = documentos[3],
                    ContoDare = contos[6],
                    ContoAvere = contos[16],
                    Imponibile = 120m,
                },
                new RigaDigitata{
                    Documento = documentos[3],
                    ContoDare = contos[6],
                    ContoAvere = contos[17],
                    Imponibile = 2457.88m,
                },
                //Documento 5
                new RigaDigitata{
                    Documento = documentos[4],
                    ContoDare = contos[7],
                    ContoAvere = contos[9],
                    VoceIva = voceivas[1],
                    AliquotaIva = aliquotas[2],
                    PercentualeAliquotaIva = aliquotas[2].Percentuale,
                    Imponibile = 1000.0m,
                    Iva = 220,
                    PercentualeIndetraibilita = 0.0m,
                    PercentualeIndeducibilita = 0.0m
                },
                new RigaDigitata{
                    Documento = documentos[4],
                    ContoDare = contos[8],
                    ContoAvere = contos[9],
                    VoceIva = voceivas[1],
                    AliquotaIva = aliquotas[2],
                    PercentualeAliquotaIva = aliquotas[2].Percentuale,
                    Imponibile = 20m,
                    Iva = 4.4m,
                    PercentualeIndetraibilita = 0.0m,
                    PercentualeIndeducibilita = 0.0m
                },
                //Documento 6
                new RigaDigitata{
                    Documento = documentos[5],
                    VoceIva = voceivas[0],
                    AliquotaIva = aliquotas[2],
                    Trattamento = TrattamentoEnum.Detraibile,
                    PercentualeAliquotaIva = aliquotas[2].Percentuale,
                    Imponibile = 1000m,
                    Iva = 220m,
                },
                new RigaDigitata{

                    Documento = documentos[5],
                    VoceIva = voceivas[0],
                    AliquotaIva = aliquotas[1],
                    Trattamento = TrattamentoEnum.Detraibile,
                    PercentualeAliquotaIva = aliquotas[1].Percentuale,
                    Imponibile = 1000m,
                    Iva = 100m,
                },
                //Documento 7
                new RigaDigitata{

                    Documento = documentos[6],
                    VoceIva = voceivas[1],
                    AliquotaIva = aliquotas[2],
                    PercentualeAliquotaIva = aliquotas[1].Percentuale,
                    Imponibile = 1000m,
                    Iva = 220m,
                },
                //Documento 8
                new RigaDigitata{

                    Documento = documentos[7],
                    ContoDare = contos[7],
                    ContoAvere = contos[9],
                    VoceIva = voceivas[1],
                    Trattamento = TrattamentoEnum.Detraibile,
                    AliquotaIva = aliquotas[2],
                    PercentualeAliquotaIva = aliquotas[1].Percentuale,
                    Imponibile = 1000m,
                    Iva = 220m,
                },
                new RigaDigitata{

                    Documento = documentos[7],
                    ContoDare = contos[8],
                    ContoAvere = contos[9],
                    VoceIva = voceivas[1],
                    Trattamento = TrattamentoEnum.Detraibile,
                    AliquotaIva = aliquotas[2],
                    PercentualeAliquotaIva = aliquotas[1].Percentuale,
                    Imponibile = 20m,
                    Iva = 4.4m,
                }


                };

                foreach (RigaDigitata r in rigaDigitatas)
                {
                    context.RigaDigitatas.Add(r);
                }
            }
            else
            {
                context.Database.ExecuteSqlCommand("delete from dbo.TemplateEffettoList");
                context.Database.ExecuteSqlCommand("delete from dbo.TemplateDocumentoList");
                context.Database.ExecuteSqlCommand("delete from dbo.TemplateEffettoRigaList");
                context.Database.ExecuteSqlCommand("delete from dbo.ApplicazioneTemplateEffettoList");
                context.Database.ExecuteSqlCommand("delete from dbo.ApplicazioneTemplateDocumentoList");
                context.Database.ExecuteSqlCommand("delete from dbo.ApplicazioneTemplateRigaList");

                context.SaveChanges();
                trans.Commit();
            }

            var applicazioneTemplateEffettoList = new ApplicazioneTemplateEffetto[]
            {
            //  Esente
                new ApplicazioneTemplateEffetto
                {
                    ContoDare = "*",
                    ContoAvere ="*",
                    VoceIva ="*",
                    Trattamento ="Esente",
                    TitoloInapplicabilita = "*",
                    Imponibile = "*",
                    RitenutaAcconto = "*",
                    Sospeso = "*",
                    Tipo = "*",
                    Caratteristica = "*",
                    Registro = "*",
                },
            //  Base
                new ApplicazioneTemplateEffetto
                {
                    ContoDare = "*",
                    ContoAvere = "*",
                    VoceIva = "*",
                    Trattamento = "Detraibile",
                    AliquotaIva = "*",
                    Imponibile = "*",
                    Iva = "*",
                    RitenutaAcconto="*",
                    Sospeso = "*",
                    Tipo = "*",
                    Caratteristica = "*",
                    Registro = "*",
                },
            //  Det/Indet (Iogg)
                new ApplicazioneTemplateEffetto
                {
                    ContoDare = "*",
                    ContoAvere = "*",
                    VoceIva = "*",
                    AliquotaIva = "*",
                    Imponibile = "*",
                    Iva = "*",
                    Trattamento="IndetraibileOggettivo",
                    PercentualeIndetraibilita = "*",
                    PercentualeIndeducibilita = "*",
                    RitenutaAcconto="*",
                    Sospeso = "*",
                    Tipo = "*",
                    Caratteristica = "*",
                    Registro = "*",
                },
            //  Base semplice
                new ApplicazioneTemplateEffetto
                {
                    ContoDare = "*",
                    ContoAvere = "*",
                    Imponibile = "*",
                    RitenutaAcconto="*",
                    Sospeso = "*",
                    Tipo = "*",
                    Caratteristica = "*",
                    Registro = "*",
                },
            //  Ritenuta
                new ApplicazioneTemplateEffetto
                {
                    RitenutaAcconto="*",
                    Sospeso = "*",
                    Tipo = "*",
                    Caratteristica = "*",
                    Registro = "*",
                },
            //  Bolla doganale
                new ApplicazioneTemplateEffetto
                {
                    VoceIva ="*",
                    AliquotaIva ="*",
                    Imponibile = "*",
                    Iva ="*",
                    Trattamento = "*",
                    RitenutaAcconto="*",
                    Sospeso = "*",
                    Tipo = DocumentoTipoEnum.BollaDoganale.ToString(),
                    Caratteristica = "*",
                    Registro =RegistroTipoEnum.Acquisti.ToString(),
                },
            //  Fattura UE
                new ApplicazioneTemplateEffetto
                {
                    VoceIva ="*",
                    AliquotaIva ="*",
                    Imponibile = "*",
                    Iva ="*",
                    RitenutaAcconto="*",
                    Sospeso = "*",
                    Tipo = DocumentoTipoEnum.Fattura.ToString(),
                    Caratteristica = DocumentoCaratteristicaEnum.FatturaUE.ToString(),
                    Registro =RegistroTipoEnum.Acquisti.ToString(),
                },
            //  Det/Indet (ISogg)
                new ApplicazioneTemplateEffetto
                {
                    ContoDare = "*",
                    ContoAvere = "*",
                    VoceIva = "*",
                    AliquotaIva = "*",
                    Imponibile = "*",
                    Iva = "*",
                    Trattamento="IndetraibileSoggettivo",
                    PercentualeIndetraibilita = "*",
                    PercentualeIndeducibilita = "*",
                    RitenutaAcconto="*",
                    Sospeso = "*",
                    Tipo = "*",
                    Caratteristica = "*",
                    Registro = "*",
                },
            //  No ritenuta
                new ApplicazioneTemplateEffetto
                {
                    RitenutaAcconto="0",
                    Sospeso = DocumentoSospensioneEnum.None.ToString(),
                    Tipo = "*",
                    Caratteristica = "*",
                    Registro = "*",
                },



            };

            foreach (ApplicazioneTemplateEffetto a in applicazioneTemplateEffettoList)
            {
                context.ApplicazioneTemplateEffettoList.Add(a);
            }

            var templateEffettos = new TemplateEffetto[]
            {
            //  Esente 
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettoList[0],
                    ContoDareId = "ContoDareId",
                    ContoAvereId = "ContoAvereId",
                    VoceIvaId = "VoceIvaId",
                    Trattamento = "*" + (int)TrattamentoEnum.Esente,
                    TitoloInapplicabilitaId = "TitoloInapplicabilitaId",
                    Valore = "#Imponibile*(1 - PercentualeIndetraibilita/100)",
                    Imponibile = "#Imponibile*(1 - PercentualeIndetraibilita/100)",
                    VariazioneFiscale = "#Imponibile*(PercentualeIndeducibilita/100)"
                },
            //  Base
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettoList[1],
                    ContoDareId = "ContoDareId",
                    ContoAvereId = "ContoAvereId",
                    Valore = "#Imponibile*(1 - PercentualeIndetraibilita/100)",
                    Imponibile = "#Imponibile*(1 - PercentualeIndetraibilita/100)",
                    Iva = "#Iva*(1 - PercentualeIndetraibilita/100)",
                    VariazioneFiscale = "#Imponibile*(PercentualeIndeducibilita/100)"
                },
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettoList[1],
                    ContoDareId = "*4",
                    ContoAvereId = "ContoAvereId",
                    VoceIvaId = "VoceIvaId",
                    Trattamento = "*" + (int)TrattamentoEnum.Detraibile,
                    AliquotaIvaId = "AliquotaIvaId",
                    Valore = "#Iva*(1 - PercentualeIndetraibilita/100)",
                    Imponibile = "#Imponibile*(1 - PercentualeIndetraibilita/100)",
                    Iva = "#Iva*(1 - PercentualeIndetraibilita/100)",
                },
                
            //  Det/Indet (IOgg)
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettoList[2],
                    ContoDareId = "ContoDareId",
                    ContoAvereId = "ContoAvereId",
                    Valore = "#Imponibile*(1 - PercentualeIndetraibilita/100)",
                    VariazioneFiscale = "#Imponibile*(1 - PercentualeIndetraibilita/100)*(PercentualeIndeducibilita/100)"
                },
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettoList[2],
                    ContoDareId = "ContoDareId",
                    ContoAvereId = "ContoAvereId",
                    Valore = "#Imponibile*(PercentualeIndetraibilita/100)",
                    VariazioneFiscale = "#Imponibile*(PercentualeIndetraibilita/100)*(PercentualeIndeducibilita/100)"
                },
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettoList[2],
                    ContoDareId = "ContoDareId",
                    ContoAvereId = "ContoAvereId",
                    VoceIvaId = "VoceIvaId",
                    Trattamento = "*" + (int)TrattamentoEnum.IndetraibileOggettivo,
                    AliquotaIvaId = "AliquotaIvaId",
                    Valore = "#Iva*(PercentualeIndetraibilita/100)",
                    VariazioneFiscale = "#Iva*(PercentualeIndetraibilita/100)*(PercentualeIndeducibilita/100)",
                    Imponibile = "#Imponibile*(PercentualeIndetraibilita/100)",
                    Iva = "#Iva*(PercentualeIndetraibilita/100)"

                },
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettoList[2],
                    ContoDareId = "*4",
                    ContoAvereId = "ContoAvereId",
                    VoceIvaId = "VoceIvaId",
                    Trattamento = "*" + (int)TrattamentoEnum.Detraibile,
                    AliquotaIvaId = "AliquotaIvaId",
                    Valore = "#Iva*(1 - PercentualeIndetraibilita/100)",
                    Imponibile = "#Imponibile*(1 - PercentualeIndetraibilita/100)",
                    Iva = "#Iva*(1 - PercentualeIndetraibilita/100)"
                },
                

            //  Base semplice
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettoList[3],
                    ContoDareId = "ContoDareId",
                    ContoAvereId = "ContoAvereId",
                    Valore = "#Imponibile",
                    VariazioneFiscale = "#Imponibile*(PercentualeIndeducibilita/100)"

                },
            //  Ritenuta
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettoList[4],
                    ContoDareId = "*10",
                    ContoAvereId = "*19",
                    Valore = "#RitenutaAcconto"
                },
                
            //  Bolla doganale
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettoList[5],
                    ContoDareId = "*4",
                    ContoAvereId = "*7",
                    VoceIvaId = "VoceIvaId",
                    Trattamento = "*" + (int)TrattamentoEnum.Detraibile,
                    AliquotaIvaId = "AliquotaIvaId",
                    Valore = "#Iva*(1 - PercentualeIndetraibilita/100)",
                    Imponibile = "#Imponibile*(1 - PercentualeIndetraibilita/100)",
                    Iva = "#Iva*(1 - PercentualeIndetraibilita/100)"

                },
            //  Fattura UE
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettoList[6],
                    ContoDareId = "*4",
                    ContoAvereId = "*7",
                    VoceIvaId = "VoceIvaId",
                    Trattamento = "*" + (int)TrattamentoEnum.Detraibile,
                    AliquotaIvaId = "AliquotaIvaId",
                    Valore = "#Iva*(1 - PercentualeIndetraibilita/100)",
                    Imponibile = "#Imponibile*(1 - PercentualeIndetraibilita/100)",
                    Iva = "#Iva*(1 - PercentualeIndetraibilita/100)",
                    VariazioneFiscale = "#Imponibile*(PercentualeIndeducibilita/100)"

                },
            //  Det/Indet (ISogg)
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettoList[7],
                    ContoDareId = "ContoDareId",
                    ContoAvereId = "ContoAvereId",
                    Valore = "#Imponibile*(1 - PercentualeIndetraibilita/100)",
                    VariazioneFiscale = "#Imponibile*(1 - PercentualeIndetraibilita/100)*(PercentualeIndeducibilita/100)"
                },
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettoList[7],
                    ContoDareId = "ContoDareId",
                    ContoAvereId = "ContoAvereId",
                    Valore = "#Imponibile*(PercentualeIndetraibilita/100)",
                    VariazioneFiscale = "#Imponibile*(PercentualeIndetraibilita/100)*(PercentualeIndeducibilita/100)"
                },
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettoList[7],
                    ContoDareId = "ContoDareId",
                    ContoAvereId = "ContoAvereId",
                    VoceIvaId = "VoceIvaId",
                    Trattamento = "*" + (int)TrattamentoEnum.IndetraibileSoggettivo,
                    AliquotaIvaId = "AliquotaIvaId",
                    Valore = "#Iva*(PercentualeIndetraibilita/100)",
                    VariazioneFiscale = "#Iva*(PercentualeIndetraibilita/100)*(PercentualeIndeducibilita/100)",
                    Imponibile = "#Imponibile*(PercentualeIndetraibilita/100)",
                    Iva = "#Iva*(PercentualeIndetraibilita/100)"

                },
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettoList[7],
                    ContoDareId = "*4",
                    ContoAvereId = "ContoAvereId",
                    VoceIvaId = "VoceIvaId",
                    Trattamento = "*" + (int)TrattamentoEnum.Detraibile,
                    AliquotaIvaId = "AliquotaIvaId",
                    Valore = "#Iva*(1 - PercentualeIndetraibilita/100)",
                    Imponibile = "#Imponibile*(1 - PercentualeIndetraibilita/100)",
                    Iva = "#Iva*(1 - PercentualeIndetraibilita/100)"
                },

            };
            context.SaveChanges();
            foreach (TemplateEffetto t in templateEffettos)
            {
                context.TemplateEffettoList.Add(t);
            }
            context.SaveChanges();

            var applicazioneTemplateDocumentoList = new ApplicazioneTemplateDocumento[]
{
               
            //  Bolla Doganale
                new ApplicazioneTemplateDocumento
                {
                    RitenutaAcconto="*",
                    Sospeso = "*",
                    Tipo = DocumentoTipoEnum.BollaDoganale.ToString(),
                    Caratteristica = "*",
                    Registro =RegistroTipoEnum.Acquisti.ToString(),
                },
            //  FatturaUe
                new ApplicazioneTemplateDocumento
                {
                    RitenutaAcconto="*",
                    Sospeso = "*",
                    Tipo = DocumentoTipoEnum.Fattura.ToString(),
                    Caratteristica = DocumentoCaratteristicaEnum.FatturaUE.ToString(),
                    Registro =RegistroTipoEnum.Acquisti.ToString(),
                },
};

            foreach (ApplicazioneTemplateDocumento a in applicazioneTemplateDocumentoList)
            {
                context.ApplicazioneTemplateDocumentoList.Add(a);
            }

            var templateEffettoDocumentoList = new TemplateDocumento[]
            {
                new TemplateDocumento
                {
                    ApplicazioneTemplateDocumento = applicazioneTemplateDocumentoList[0],
                    Totale = "*1800",
                    RitenutaAcconto ="§RitenutaAcconto",
                    Sospeso ="§Sospeso" ,
                    Tipo = "*" + (int)DocumentoTipoEnum.Fattura ,
                    Caratteristica ="*" + (int)DocumentoCaratteristicaEnum.Normale,
                    Registro="*" + (int)RegistroTipoEnum.Finanziari,
                    CliforId="§CliforId"
                },
                new TemplateDocumento
                {
                    ApplicazioneTemplateDocumento = applicazioneTemplateDocumentoList[0],
                    Totale = "§Totale",
                    RitenutaAcconto ="§RitenutaAcconto",
                    Sospeso ="§Sospeso" ,
                    Tipo = "*" + (int)DocumentoTipoEnum.Fattura ,
                    Caratteristica ="*" + (int)DocumentoCaratteristicaEnum.Normale,
                    Registro="*" + (int)RegistroTipoEnum.Finanziari,
                    CliforId="§CliforId"
                },
                new TemplateDocumento
                {
                    ApplicazioneTemplateDocumento = applicazioneTemplateDocumentoList[1],
                    Totale = "§Totale",
                    RitenutaAcconto ="§RitenutaAcconto",
                    Sospeso ="§Sospeso" ,
                    Tipo = "*" + (int)DocumentoTipoEnum.Contabile ,
                    Caratteristica ="*" + (int)DocumentoCaratteristicaEnum.Autofattura,
                    Registro="*" + (int)RegistroTipoEnum.Emesse,
                    CliforId="§CliforId"
                },

            };

            foreach (TemplateDocumento t in templateEffettoDocumentoList)
            {
                context.TemplateDocumentoList.Add(t);
            }


            var applicazioneTemplateRigaList = new ApplicazioneTemplateRiga[]
            {

                new ApplicazioneTemplateRiga
                {

                    VoceIva ="*",
                    AliquotaIva ="*",
                    Imponibile = "*",
                    Iva ="*",
                    Trattamento = "*",

                    RitenutaAcconto = "0.0m",
                    Sospeso = DocumentoSospensioneEnum.None.ToString(),
                    Tipo = DocumentoTipoEnum.BollaDoganale.ToString(),
                    Caratteristica = DocumentoCaratteristicaEnum.Normale.ToString(),
                    Registro = RegistroTipoEnum.Acquisti.ToString(),

                    RitenutaAccontoEffetto = "0.0m",
                    SospesoEffetto = DocumentoSospensioneEnum.None.ToString(),
                    TipoEffetto = DocumentoTipoEnum.Fattura.ToString(),
                    CaratteristicaEffetto = DocumentoCaratteristicaEnum.Normale.ToString(),
                    RegistroEffetto = RegistroTipoEnum.Finanziari.ToString(),

                },
                new ApplicazioneTemplateRiga
                {

                    VoceIva ="*",
                    AliquotaIva ="*",
                    Imponibile = "*",
                    Iva ="*",

                    RitenutaAcconto = "0.0m",
                    Sospeso = DocumentoSospensioneEnum.None.ToString(),
                    Tipo = DocumentoTipoEnum.Fattura.ToString(),
                    Caratteristica = DocumentoCaratteristicaEnum.Autofattura.ToString(),
                    Registro = RegistroTipoEnum.Acquisti.ToString(),

                    RitenutaAccontoEffetto = "0.0m",
                    SospesoEffetto = DocumentoSospensioneEnum.None.ToString(),
                    TipoEffetto = DocumentoTipoEnum.Contabile.ToString(),
                    CaratteristicaEffetto = DocumentoCaratteristicaEnum.Autofattura.ToString(),
                    RegistroEffetto = RegistroTipoEnum.Emesse.ToString(),

                },
                new ApplicazioneTemplateRiga
                {

                    VoceIva ="Spese",
                    AliquotaIva ="*",
                    Imponibile = "*",
                    Iva ="*",
                    //
                    RitenutaAcconto = "0.0m",
                    Sospeso = DocumentoSospensioneEnum.None.ToString(),
                    Tipo = DocumentoTipoEnum.Fattura.ToString(),
                    Caratteristica = DocumentoCaratteristicaEnum.Autofattura.ToString(),
                    Registro = RegistroTipoEnum.Acquisti.ToString(),
                    //
                    RitenutaAccontoEffetto = "0.0m",
                    SospesoEffetto = DocumentoSospensioneEnum.None.ToString(),
                    TipoEffetto = DocumentoTipoEnum.Fattura.ToString(),
                    CaratteristicaEffetto = DocumentoCaratteristicaEnum.Autofattura.ToString(),
                    RegistroEffetto = RegistroTipoEnum.Emesse.ToString(),

                },
            };

            foreach (ApplicazioneTemplateRiga a in applicazioneTemplateRigaList)
            {
                context.ApplicazioneTemplateRigaList.Add(a);
            }


            var templateEffettoRigaList = new TemplateRiga[]
            {
                new TemplateRiga
                {
                    ApplicazioneTemplateRiga = applicazioneTemplateRigaList[0],
                    ContoDareId ="*1",
                    ContoAvereId ="*10",
                    PercentualeAliquotaIva = "&PercentualeAliquotaIva",
                    Imponibile ="*999" ,
                    Iva="*0"
                },
                new TemplateRiga
                {
                    ApplicazioneTemplateRiga = applicazioneTemplateRigaList[1],
                    ContoDareId ="*20",
                    ContoAvereId ="*10",
                    Imponibile ="*1000" ,
                    Iva="*0"
                },
                new TemplateRiga
                {
                    ApplicazioneTemplateRiga = applicazioneTemplateRigaList[2],
                    PercentualeAliquotaIva = "&PercentualeAliquotaIva",
                    Imponibile ="*1000" ,
                    Iva="&Iva"
                }
            };

            foreach (TemplateRiga t in templateEffettoRigaList)
            {
                context.TemplateEffettoRigaList.Add(t);
            }

            context.SaveChanges();



        }
    }
}
