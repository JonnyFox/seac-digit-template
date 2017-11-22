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
    public class ApplicazioneTemplateDocumentoService
    {
        SeacDigitTemplateContext _ctx;

        public ApplicazioneTemplateDocumentoService(SeacDigitTemplateContext ctx)
        {
            _ctx = ctx;
        }

        public Task<List<ApplicazioneTemplateDocumento>> GetAll()
        {
            return _ctx.ApplicazioneTemplateDocumentoList.ToListAsync();
        }

        public Task<List<ApplicazioneTemplateDocumento>> GetById(int id)
        {
            return _ctx.ApplicazioneTemplateDocumentoList.Where(c => c.Id == id).ToListAsync();
        }

        public async Task<ApplicazioneTemplateDocumento> GetTemplateAsync(RigaDigitata rigaDigitata, Documento documento)
        {
            var query = _ctx.ApplicazioneTemplateDocumentoList.AsQueryable();

            query = documento.RitenutaAcconto == null ? query.Where(a => a.RitenutaAcconto == null) : query.Where(a => a.RitenutaAcconto != null);
            query = query.Where(a => a.Sospeso == documento.Sospeso.ToString() || a.Sospeso == "*");
            query = query.Where(a => a.Tipo == documento.Tipo.ToString() || a.Tipo == "*");
            query = query.Where(a => a.Caratteristica == documento.Caratteristica.ToString() || a.Caratteristica == "*");
            query = query.Where(a => a.Registro == documento.Registro.ToString() || a.Registro == "*");

            query = rigaDigitata.ContoDareId == null ? query.Where(a => a.ContoDare == null) : query.Where(a => a.ContoDare != null);
            query = rigaDigitata.ContoAvereId == null ? query.Where(a => a.ContoAvere == null) : query.Where(a => a.ContoAvere != null);
            query = rigaDigitata.VoceIvaId == null ? query.Where(a => a.VoceIva == null) : query.Where(a => a.VoceIva != null);
            query = rigaDigitata.Trattamento == null ? query.Where(a => a.Trattamento == null) : query.Where(a => a.Trattamento != null);
            query = rigaDigitata.TitoloInapplicabilitaId == null ? query.Where(a => a.TitoloInapplicabilita == null) : query.Where(a => a.TitoloInapplicabilita != null);
            query = rigaDigitata.AliquotaIvaId == null ? query.Where(a => a.AliquotaIva == null) : query.Where(a => a.AliquotaIva != null);
            query = rigaDigitata.Imponibile == null ? query.Where(a => a.Imponibile == null) : query.Where(a => a.Imponibile != null);
            query = rigaDigitata.Iva == null ? query.Where(a => a.Iva == null) : query.Where(a => a.Iva == rigaDigitata.Iva.ToString() || a.Iva == "*");

            var applicationTemplates = await query.ToListAsync();

            if (applicationTemplates.Count == 0)
            {
                Console.WriteLine("No template found");
                return null;

            }
            else if (applicationTemplates.Count > 1)
            {
                Console.WriteLine("More than  one template found");
                return applicationTemplates.OrderBy(x => CountStar(x)).First();
            }
            else
            {
                return applicationTemplates.First();
            }
        }



        int CountStar(ApplicazioneTemplateDocumento applicationTamplateDocumento)
        {
            int stars = 0;
            foreach (PropertyInfo pi in applicationTamplateDocumento.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(applicationTamplateDocumento);
                    if (value == "*")
                    {
                        stars++;
                    }
                }
            }
            return stars;

        }
    }
}
