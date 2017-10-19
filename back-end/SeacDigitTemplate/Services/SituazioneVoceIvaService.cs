using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Services
{
    public class SituazioneVoceIvaService
    {
        SeacDigitTemplateContex _ctx;
        public SituazioneVoceIvaService(SeacDigitTemplateContex ctx)
        {
            _ctx = ctx;
        }

        public Task<List<SituazioneVoceIVA>> GetAll()
        {
            return _ctx.SituazioneVoceIVAs.ToListAsync();
        }
    }
}
