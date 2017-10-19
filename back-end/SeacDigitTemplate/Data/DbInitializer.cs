﻿
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Models;
using System.Linq;

namespace SeacDigitTemplate.Data
{
    public class DbInitializer
    {
        public static void Initialize(SeacDigitTemplateContex context)
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
                  new Conto{Nome ="Merce"},
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

              foreach (Clifor c  in clifors)
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
                context.AliquotaIVAs.Add(a);
            }
            

             var voceivas = new VoceIva[]
             {
                 new VoceIva{Nome = "Merce"},
                 new VoceIva{Nome = "Spese"},
                 new VoceIva{Nome = "Consulenza"}
             };

             foreach(VoceIva v in voceivas)
             {
                 context.VoceIVAs.Add(v);

             }

            var documentos = new Documento[]
            {
                new Documento{Totale = 100.0m, RitenutaAcconto = 35.0m, Sospeso = DocumentoSospensioneEnum.ContoIVA, Tipo = DocumentoTipoEnum.Fattura, Caratteristica = DocumentoCaratteristicaEnum.Autofattura, Clifor = clifors[0], Registro = RegistroTipoEnum.Corrispettivi, Numero = "1", Protocollo = 45},
                new Documento{Totale = 45.0m, RitenutaAcconto = 5.0m, Sospeso = DocumentoSospensioneEnum.Conto, Tipo = DocumentoTipoEnum.Contabile, Caratteristica = DocumentoCaratteristicaEnum.FatturaUE, Clifor = clifors[1], Registro = RegistroTipoEnum.Emesse, Numero = "2", Protocollo = 5}
            };

            foreach(var d in documentos)
            {
                context.Documentos.Add(d);
            }

            var titoloInapplicabilitas = new TitoloInapplicabilita[]
             {
                 new TitoloInapplicabilita{Nome = "Merce"},
                 new TitoloInapplicabilita{Nome = "Spese"},
                 new TitoloInapplicabilita{Nome = "Consulenza"}
             };

            foreach(TitoloInapplicabilita t in titoloInapplicabilitas)
            {
                context.TitoloInapplicabilitas.Add(t);
            }

            var rigaDigitatas = new RigaDigitata[]
            {
                new RigaDigitata{ Documento = documentos[0], ContoDare = contos[1], ContoAvere = contos[2], VoceIVA = voceivas[1], AliquotaIVA = aliquotas[2], Trattamento = TrattamentoEnum.None, TitoloInapplicabilita = null, IVA = 220.0m, PercentualeIndeducibilita = 0.0m, PercentualeIndetraibilita = 0.0m, Settore = 0, Note = null },
                new RigaDigitata{ Documento = documentos[0], ContoDare = contos[2], ContoAvere = contos[2], VoceIVA = voceivas[1], AliquotaIVA = aliquotas[2], Trattamento = TrattamentoEnum.Detraibile, TitoloInapplicabilita = null,Imponibile = 500.0m, IVA = 110, PercentualeIndeducibilita = 0.0m, PercentualeIndetraibilita = 0.0m, Settore = 0, Note = null },
            };

            foreach(RigaDigitata r in rigaDigitatas)
            {
                context.RigaDigitatas.Add(r);
            }

            context.SaveChanges();
        }
    }
}
