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
        SeacDigitTemplateContext _ctx;
        public VoceIvaService(SeacDigitTemplateContext ctx)
        {
            _ctx = ctx;
        }

        public Task<List<VoceIva>> GetAll()
        {
            return _ctx.VoceIvas.ToListAsync();
        }
        public Task<List<VoceIva>> GetById(int id)
        {
            return _ctx.VoceIvas.Where(i => i.Id == id).ToListAsync();
        }
    }
}
