using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Services
{
    public class ApplicazioneTemplateEffettoService
    {
        SeacDigitTemplateContext _ctx;

        public ApplicazioneTemplateEffettoService(SeacDigitTemplateContext ctx)
        {
            _ctx = ctx;
        }

        public Task<List<ApplicazioneTemplateEffetto>> GetAll()
        {
            return _ctx.ApplicazioneTemplateEffettoList.ToListAsync();
        }

        public Task<List<ApplicazioneTemplateEffetto>> GetById(int id)
        {
            return _ctx.ApplicazioneTemplateEffettoList.Where(c => c.Id == id).ToListAsync();
        }

        public async Task<ApplicazioneTemplateEffetto> GetTemplateAsync(RigaDigitata rigaDigitata, Documento documento)
        {
            var query = _ctx.ApplicazioneTemplateEffettoList.AsQueryable();

            query = documento.RitenutaAcconto == null ? query.Where(a => a.RitenutaAcconto == null) : query.Where(a => a.RitenutaAcconto == documento.RitenutaAcconto.ToString() || a.RitenutaAcconto == "*");
            query = query.Where(a => a.Sospeso == documento.Sospeso.ToString() || a.Sospeso == "*");
            query = query.Where(a => a.Tipo == documento.Tipo.ToString() || a.Tipo == "*");
            query = query.Where(a => a.Caratteristica == documento.Caratteristica.ToString() || a.Caratteristica== "*");
            query = query.Where(a => a.Registro == documento.Registro.ToString() || a.Registro == "*");
          
            query = rigaDigitata.ContoDareId == null ? query.Where(a => a.ContoDare == null) : query.Where(a => a.ContoDare != null);
            query = rigaDigitata.ContoAvereId == null ? query.Where(a => a.ContoAvere == null) : query.Where(a => a.ContoAvere != null);
            query = rigaDigitata.VoceIvaId == null ? query.Where(a => a.VoceIva == null) : query.Where(a => a.VoceIva != null);
            query = rigaDigitata.Trattamento == null ? query.Where(a => a.Trattamento == null) : query.Where(a => a.Trattamento == rigaDigitata.Trattamento.ToString() || a.Trattamento == "*" );
            query = rigaDigitata.TitoloInapplicabilitaId == null ? query.Where(a => a.TitoloInapplicabilita == null) : query.Where(a => a.TitoloInapplicabilita != null);
            query = rigaDigitata.AliquotaIvaId == null ? query.Where(a => a.AliquotaIva == null) : query.Where(a => a.AliquotaIva != null);
            query = rigaDigitata.Imponibile == null ? query.Where(a => a.Imponibile == null) : query.Where(a => a.Imponibile != null);
            query = rigaDigitata.Iva == null ? query.Where(a => a.Iva == null) : query.Where(a => a.Iva == rigaDigitata.Iva.ToString() || a.Iva == "*");

            var applicationTemplateList = await query.ToListAsync();

            if (applicationTemplateList.Count == 0)
            {
                Console.WriteLine("No template found");
                return null;

            }
            else if (applicationTemplateList.Count > 1)
            {
                Console.WriteLine("More than  one template found");

                //var bestapplicationtemplates = applicationTemplates.GetType().GetProperties()
                //.Where(pi => pi.GetValue(applicationTemplates) is string)
                //.Select(pi => (string)pi.GetValue(applicationTemplates))
                //.Count(value => value=="*");
                return applicationTemplateList.OrderBy(x => CountStar(x)).First();
            }
            else  
            {
                return applicationTemplateList.First();
            }    
        }



        int CountStar(ApplicazioneTemplateEffetto applicationTamplateEffetto)
        {
            int stars = 0;
            foreach (PropertyInfo pi in applicationTamplateEffetto.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(applicationTamplateEffetto);
                    if (value =="*")
                    {
                        stars++;
                    }
                }
            }
            return stars;
            
        }
    }
}
