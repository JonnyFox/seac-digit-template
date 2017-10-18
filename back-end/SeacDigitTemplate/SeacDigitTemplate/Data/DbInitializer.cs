using SeacDigitTemplate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Data
{
    public class DbInitializer
    {
        public static void Initialize(SeacDigitTemplateContex context)
        {
            context.Database.EnsureCreated();

            if (context.Contos.Any())
            {
                return;
            }

            //var users = new User[]
            //{
            //    new User{ Name = "Hello", Password ="Hello" }
            //};

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
            context.SaveChanges();
        }
    }
}
