using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Services
{
    public class ContoService
    {
        SeacDigitTemplateContext _ctx;
        public ContoService( SeacDigitTemplateContext ctx)
        {
            _ctx = ctx;
        }

        public Task<List<Conto>> GetAll()
        {
            return _ctx.Contos.ToListAsync();
        }

        public Task<List<Conto>> GetById(int id)
        {
            return _ctx.Contos.Where(c => c.Id == id).ToListAsync();
        }
    }
}
