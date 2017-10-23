using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using System.Collections.Generic;
using System.Linq;
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
        public Task<List<VoceIva>> GetById(int id)
        {
            return _ctx.VoceIVAs.Where(i => i.Id == id).ToListAsync();
        }
    }
}
