using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Services
{
    public class CliforService
    {
        SeacDigitTemplateContext _ctx;
        public CliforService(SeacDigitTemplateContext ctx)
        {
            _ctx = ctx;
        }

        public Task<List<Clifor>> GetAll()
        {
            return _ctx.Clifors.ToListAsync();
        }

        public Task<List<Clifor>> GetById(int id)
        {
            return _ctx.Clifors.Where(c => c.Id == id).ToListAsync();
        }
    }
}
