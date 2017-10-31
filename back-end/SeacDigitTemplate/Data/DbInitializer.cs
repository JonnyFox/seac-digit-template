
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Models;
using System.Linq;

namespace SeacDigitTemplate.Data
{
    public class DbInitializer
    {
        public static void Initialize(SeacDigitTemplateContext context)
        {
            context.Database.EnsureCreated();

            if (context.Clifors.Any())
            {
                return;
            }

            var contos = new Conto[]
            {
                  new Conto{Nome ="Merce"},
                  new Conto{Nome ="Merce2"},
                  new Conto{Nome ="Valori Bollati"},
                  new Conto{Nome ="Iva cr"},
                  new Conto{Nome ="Salari/Stipendi"},
                  new Conto{Nome ="Anticipo IVS"},
                  new Conto{Nome ="Transitorio"},
                  new Conto{Nome ="Compenso"},
                  new Conto{Nome ="Contributo Previdenziale"},
                  new Conto{Nome ="Fornitore"},
                  new Conto{Nome ="Spese"},
                  new Conto{Nome ="Iva cr(s)"},
                  new Conto{Nome ="Iva Transitorio"}
            };
            foreach (Conto c in contos)
            {
                context.Contos.Add(c);
            }


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
                new AliquotaIva{Percentuale = 0.04m},
                new AliquotaIva{Percentuale = 0.10m},
                new AliquotaIva{Percentuale = 0.22m}
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
                    Totale = 45.0m, RitenutaAcconto = 5.0m, Sospeso = DocumentoSospensioneEnum.Conto, Tipo = DocumentoTipoEnum.Contabile, Caratteristica = DocumentoCaratteristicaEnum.FatturaUE, Clifor = clifors[1], Registro = RegistroTipoEnum.Emesse, Numero = "2", Protocollo = 5},
                new Documento{Totale = 45.0m, RitenutaAcconto = 5.0m, Sospeso = DocumentoSospensioneEnum.Conto, Tipo = DocumentoTipoEnum.Contabile, Caratteristica = DocumentoCaratteristicaEnum.FatturaUE, Clifor = clifors[1], Registro = RegistroTipoEnum.Emesse, Numero = "2", Protocollo = 5}
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
                    PercentualeIndetraibilita = 0.4m,
                    PercentualeIndeducibilita = 0.2m
                }
            };

            foreach (RigaDigitata r in rigaDigitatas)
            {
                context.RigaDigitatas.Add(r);
            }

            var applicazioneTemplateEffettos = new ApplicazioneTemplateEffetto[]
            {
                //new ApplicazioneTemplateEffetto
                //{
                //    ContoDare = "*",
                //    ContoAvere ="Fornitore",
                //    VoceIva ="Merce",
                //    Trattamento = "*",
                //    AliquotaIva = "*",
                //    Imponibile = "*",
                //    Iva = "*"
                //},
                new ApplicazioneTemplateEffetto
                {
                    ContoDare = "Valori Bollati",
                    ContoAvere ="Fornitore",
                    VoceIva ="Merce",
                    TitoloInapplicabilita = "*",
                    Imponibile = "*"
                },
                new ApplicazioneTemplateEffetto
                {
                    ContoDare = "*",
                    ContoAvere = "*",
                    VoceIva = "*",
                    Trattamento = "*", // trattamento should be D check the query
                    AliquotaIva = "*",
                    Imponibile = "*",
                    Iva = "*",
                    PercentualeIndetraibilita = "*",
                    PercentualeIndeducibilita = "*"
                }
            };

            foreach (ApplicazioneTemplateEffetto a in applicazioneTemplateEffettos)
            {
                context.ApplicazioneTemplateEffettos.Add(a);
            }

            var templateEffettos = new TemplateEffetto[]
            {
                //new TemplateEffetto
                //{
                //    ApplicazioneTemplateEffetto = applicazioneTemplateEffettos[0],
                //    ContoDareId = "ContoDareId",
                //    ContoAvereId = "ContoAvereId",
                //    Valore = "Imponibile"
                //},
                //new TemplateEffetto
                //{
                //    ApplicazioneTemplateEffetto = applicazioneTemplateEffettos[0],
                //    ContoDareId = "*3",
                //    ContoAvereId = "ContoAvereId",
                //    VoceIvaId = "VoceIvaId",
                //    AliquotaIvaId = "AliquotaIvaId",
                //    Valore = "Imponibile",
                //    Imponibile = "Imponibile",
                //    Iva = "Iva"
                //},
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettos[0],
                    ContoDareId = "ContoDareId",
                    ContoAvereId = "ContoAvereId",
                    VoceIvaId = "VoceIvaId",
                    TitoloInapplicabilitaId = "TitoloInapplicabilita",
                    Valore = "Imponibile"
                },
                new TemplateEffetto // second row
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettos[1],
                    ContoDareId = "ContoDareId",
                    ContoAvereId = "ContoAvereId",
                    Valore = "#Imponibile*PercentualeIndetraibilita",
                    VariazioneF = "#Imponibile*PercentualeIndetraibilita*PercentualeIndeducibilita"
                },
                new TemplateEffetto // first row 
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettos[1],
                    ContoDareId = "ContoDareId",
                    ContoAvereId = "ContoAvereId",
                    Valore = "#Imponibile*(1 - PercentualeIndetraibilita)",
                    VariazioneF = "#Imponibile*(1 - PercentualeIndetraibilita)*PercentualeIndeducibilita"
                },
                new TemplateEffetto // third row
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettos[1],
                    ContoDareId = "ContoDareId",
                    ContoAvereId = "ContoAvereId",
                    VoceIvaId = "VoceIvaId",
                    Trattamento = "*" + (int)TrattamentoEnum.IndetraibileOggettivo,
                    AliquotaIvaId = "AliquotaIvaId",
                    Valore = "#Iva*PercentualeIndetraibilita",
                    VariazioneF = "#Iva*PercentualeIndetraibilita*PercentualeIndeducibilita",
                    Imponibile = "Imponibile",
                    Iva = "#Iva*PercentualeIndetraibilita"
                },
                new TemplateEffetto
                {
                    ApplicazioneTemplateEffetto = applicazioneTemplateEffettos[1],
                    ContoDareId = "*3",
                    ContoAvereId = "ContoAvereId",
                    VoceIvaId = "VoceIvaId",
                    Trattamento = "*" + (int)TrattamentoEnum.Detraibile,
                    AliquotaIvaId = "AliquotaIvaId",
                    Valore = "#Iva*(1 - PercentualeIndetraibilita)",
                    Imponibile = "Imponibile",
                    Iva = "#Iva*(1 - PercentualeIndetraibilita)"
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
