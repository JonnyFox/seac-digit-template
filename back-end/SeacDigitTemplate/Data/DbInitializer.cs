using SeacDigitTemplate.Model;
using System.Linq;

namespace SeacDigitTemplate.Data
{
    public class DbInitializer
    {
        public static void Initialize(SeacDigitTemplateContex context)
        {
            context.Database.EnsureCreated();

            /*  if (context.Contos.Any())
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
              context.SaveChanges();*/

            /*  if (context.Clifors.Any())
              {
                  return;
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

              context.SaveChanges();*/

            /*var aliquotas = new AliquotaIva[]
            {
                new AliquotaIva{Percentuale = 0.04m},
                new AliquotaIva{Percentuale = 0.10m},
                new AliquotaIva{Percentuale = 0.22m}
            };

            foreach (AliquotaIva a in aliquotas)
            {
                context.AliquotaIVAs.Add(a);
            }

            context.SaveChanges();*/

           /* var voceivas = new VoceIva[]
            {
                new VoceIva{Nome = "Merce"},
                new VoceIva{Nome = "Spese"},
                new VoceIva{Nome = "Consulenza"}
            };

            foreach(VoceIva v in voceivas)
            {
                context.VoceIVAs.Add(v);
                   
            }

            context.SaveChanges();*/

        }
    }
}
