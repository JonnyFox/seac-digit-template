using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return _ctx.ApplicazioneTemplateEffettos.ToListAsync();
        }

        public Task<List<ApplicazioneTemplateEffetto>> GetById(int id)
        {
            return _ctx.ApplicazioneTemplateEffettos.Where(c => c.Id == id).ToListAsync();
        }

        public async Task<ApplicazioneTemplateEffetto> GetTemplateAsync(RigaDigitata rigaDigitata)
        {
            var query = _ctx.ApplicazioneTemplateEffettos.AsQueryable();

            query = rigaDigitata.ContoDareId == null ? query.Where(a => a.ContoDare == null) : query.Where(a => a.ContoDare != null);
            query = rigaDigitata.ContoAvereId == null ? query.Where(a => a.ContoAvere == null) : query.Where(a => a.ContoAvere != null);
            query = rigaDigitata.VoceIvaId == null ? query.Where(a => a.VoceIva == null) : query.Where(a => a.VoceIva != null);
            query = rigaDigitata.Trattamento == null ? query.Where(a => a.Trattamento == null) : query.Where(a => a.Trattamento != null);
            query = rigaDigitata.TitoloInapplicabilitaId == null ? query.Where(a => a.TitoloInapplicabilita == null) : query.Where(a => a.TitoloInapplicabilita != null);
            query = rigaDigitata.AliquotaIvaId == null ? query.Where(a => a.AliquotaIva == null) : query.Where(a => a.AliquotaIva != null);
            query = rigaDigitata.Imponibile == null ? query.Where(a => a.Imponibile == null) : query.Where(a => a.Imponibile != null);
            query = rigaDigitata.Iva == null ? query.Where(a => a.Iva == null) : query.Where(a => a.Iva != null);

            var applicationTemplates = await query.ToListAsync();

            if (applicationTemplates.Count == 0)
            {
                Console.WriteLine("No template found");
                return null;
            }

            if (applicationTemplates.Count > 1)
            {
                Console.WriteLine("More than  one template found");
            }

            return applicationTemplates.First();
        }
    }
}
