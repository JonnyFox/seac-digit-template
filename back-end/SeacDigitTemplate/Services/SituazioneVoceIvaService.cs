using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SeacDigitTemplate.Services
{
    public class SituazioneVoceIvaService
    {
        SeacDigitTemplateContext _ctx;
        public SituazioneVoceIvaService(SeacDigitTemplateContext ctx)
        {
            _ctx = ctx;
        }

        public Task<List<SituazioneVoceIva>> GetAll()
        {
            return _ctx.SituazioneVoceIVAs.ToListAsync();
        }
        public Task<List<SituazioneVoceIva>> GetById(int id)
        {
            return _ctx.SituazioneVoceIVAs.Where(i => i.AliquotaIvaId == id).ToListAsync();
        }
    }
}
