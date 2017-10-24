using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Services
{
    public class AliquotaIvaService
    {
        SeacDigitTemplateContext _ctx;
        public AliquotaIvaService(SeacDigitTemplateContext ctx)
        {
            _ctx = ctx;
        }

        public Task<List<AliquotaIva>> GetAll()
        {
            return _ctx.AliquotaIvas.ToListAsync();
        }
        public Task<List<AliquotaIva>> GetById(int id)
        {
            return _ctx.AliquotaIvas.Where(c => c.Id == id).ToListAsync();
        }
    }
}
