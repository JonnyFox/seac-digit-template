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
        SeacDigitTemplateContex _ctx;
        public AliquotaIvaService(SeacDigitTemplateContex ctx)
        {
            _ctx = ctx;
        }

        public Task<List<AliquotaIva>> GetAll()
        {
            return _ctx.AliquotaIVAs.ToListAsync();
        }
        public Task<List<AliquotaIva>> GetById(int id)
        {
            return _ctx.AliquotaIVAs.Where(c => c.Id == id).ToListAsync();
        }
    }
}
