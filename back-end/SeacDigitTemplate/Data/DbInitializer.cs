
using SeacDigitTemplate.Model;
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

            if (context.Clifors.Any())
            {
                return;
            }

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
                  new Conto{ Id = 13, Nome ="Iva Transitorio"}
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
                    RitenutaAcconto = 0.0m,
                    Sospeso = DocumentoSospensioneEnum.None,
                    Tipo = DocumentoTipoEnum.Fattura,
                    Caratteristica = DocumentoCaratteristicaEnum.Normale,
                    Clifor = clifors[0],
                    Registro = RegistroTipoEnum.Emesse,
                    Numero = "1",
                    Protocollo = 1
                   
                },
                new Documento{
                    Totale = 45.0m,
                    RitenutaAcconto = 5.0m,
                    Sospeso = DocumentoSospensioneEnum.Conto,
                    Tipo = DocumentoTipoEnum.Contabile,
                    Caratteristica = DocumentoCaratteristicaEnum.FatturaUE,
                    Clifor = clifors[1],
                    Registro = RegistroTipoEnum.Emesse,
                    Numero = "2",
                    Protocollo = 5
                    },
                new Documento{
                    Totale = 45.0m,
                    RitenutaAcconto = 5.0m,
                    Sospeso = DocumentoSospensioneEnum.Conto,
                    Tipo = DocumentoTipoEnum.Contabile,
                    Caratteristica = DocumentoCaratteristicaEnum.FatturaUE,
                    Clifor = clifors[1],
                    Registro = RegistroTipoEnum.Emesse,
                    Numero = "2",
                    Protocollo = 5
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
                    TitoloInapplicabilita = titoloInapplicabilitas[2],
                    Imponibile = 2.0m
                },
                new RigaDigitata{
                    Documento = documentos[1],
                    ContoDare = contos[0],
                    ContoAvere = contos[9],
                    VoceIva = voceivas[0],
                    AliquotaIva = aliquotas[2],
                    PercentualeAliquotaIva = aliquotas[2].Percentuale,
                    Imponibile = 1000.0m,
                    Iva = 220,
                    PercentualeIndetraibilita = 0.4m
                },
                 new RigaDigitata{
                    Documento = documentos[2],
                    ContoDare = contos[0],
                    ContoAvere = contos[9],
                    VoceIva = voceivas[0],
                    Trattamento = TrattamentoEnum.Detraibile,
                    AliquotaIva = aliquotas[2],
                    PercentualeAliquotaIva = aliquotas[2].Percentuale,
                    Imponibile = 1000.0m,
                    Iva = 220,
                    PercentualeIndetraibilita = 40m,
                    PercentualeIndeducibilita = 20m
                }
            };

            foreach (RigaDigitata r in rigaDigitatas)
            {
                context.RigaDigitatas.Add(r);
            }

            var applicazioneTemplateEffettos = new ApplicazioneTemplateEffetto[]
            {
                new ApplicazioneTemplateEffetto
                {
                    ContoDare = "Valori Bollati",
                    ContoAvere ="Fornitore",
                    VoceIva ="Merce",
                    TitoloInapplicabilita = "*",
                    Imponibile = "*",
                    RitenutaAcconto = "0.0m",
                    Sospeso = DocumentoSospensioneEnum.None.ToString(),
                    Tipo = DocumentoTipoEnum.Fattura.ToString(),
                    Caratteristica = DocumentoCaratteristicaEnum.Normale.ToString(),
                    Registro = RegistroTipoEnum.Emesse.ToString(),
                },
                new ApplicazioneTemplateEffetto
                {
                    ContoDare = "*",
                    ContoAvere = "*",
                    VoceIva = "*",
                    Trattamento = TrattamentoEnum.Detraibile.ToString(),
                    AliquotaIva = "*",
                    Imponibile = "*",
                    Iva = "*",
                    PercentualeIndetraibilita = "*",
                    PercentualeIndeducibilita = "*",
                    RitenutaAcconto = "0.0m",
                    Sospeso = DocumentoSospensioneEnum.None.ToString(),
                    Tipo = DocumentoTipoEnum.Fattura.ToString(),
                    Caratteristica = DocumentoCaratteristicaEnum.Normale.ToString(),
                    Registro = RegistroTipoEnum.Emesse.ToString(),
                }
            };

            foreach (ApplicazioneTemplateEffetto a in applicazioneTemplateEffettos)
            {
                context.ApplicazioneTemplateEffettos.Add(a);
            }

            var templateEffettos = new TemplateEffetto[]
            {
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettos[0],
                    ContoDareId = "ContoDareId",
                    ContoAvereId = "ContoAvereId",
                    VoceIvaId = "VoceIvaId",
                    TitoloInapplicabilitaId = "TitoloInapplicabilitaId",
                    Valore = "Imponibile",
                    Iva = "Imponibile"
                },
                new TemplateEffetto // second row
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettos[1],
                    ContoDareId = "ContoDareId",
                    ContoAvereId = "ContoAvereId",
                    Valore = "#Imponibile*PercentualeIndetraibilita/100",
                    VariazioneFiscale = "#Imponibile*PercentualeIndetraibilita/100*PercentualeIndeducibilita/100"
                },
                new TemplateEffetto // first row 
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettos[1],
                    ContoDareId = "ContoDareId",
                    ContoAvereId = "ContoAvereId",
                    Valore = "#Imponibile*(1 - PercentualeIndetraibilita/100)",
                    VariazioneFiscale = "#Imponibile*(1 - PercentualeIndetraibilita/100)*PercentualeIndeducibilita/100"
                },
                new TemplateEffetto // third row
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettos[1],
                    ContoDareId = "ContoDareId",
                    ContoAvereId = "ContoAvereId",
                    VoceIvaId = "VoceIvaId",
                    Trattamento = "*" + (int)TrattamentoEnum.IndetraibileOggettivo,
                    AliquotaIvaId = "AliquotaIvaId",
                    Valore = "#Iva*PercentualeIndetraibilita/100",
                    VariazioneFiscale = "#Iva*PercentualeIndetraibilita/100*PercentualeIndeducibilita/100",
                    Imponibile = "#Imponibile*PercentualeIndetraibilita/100",
                    Iva = "#Iva*PercentualeIndetraibilita/100"
                },
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettos[1],
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

            foreach (TemplateEffetto t in templateEffettos)
            {
                context.TemplateEffettos.Add(t);
            }

            context.SaveChanges();
        }
    }
}
