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

            if (context.Documentos.Any())
            {
                return;
            }

            //var users = new User[]
            //{
            //    new User{ Name = "Hello", Password ="Hello" }
            //};


            context.SaveChanges();
        }
    }
}
