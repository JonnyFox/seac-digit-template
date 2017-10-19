using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Services
{
    public class VoceIvaService
    {
        SeacDigitTemplateContex _ctx;
        public VoceIvaService(SeacDigitTemplateContex ctx)
        {
            _ctx = ctx;
        }

        public Task<List<VoceIva>> GetAll()
        {
            return _ctx.VoceIVAs.ToListAsync();
        }
    }
}
