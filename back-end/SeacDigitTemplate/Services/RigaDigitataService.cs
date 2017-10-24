using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Services
{
    public class RigaDigitataService
    {
        SeacDigitTemplateContex _ctx;
        public RigaDigitataService(SeacDigitTemplateContex ctx)
        {
            _ctx = ctx;
        }

        public Task<List<RigaDigitata>> GetAll()
        {
            return _ctx.RigaDigitatas.ToListAsync();
        }
        public Task<List<RigaDigitata>> GetById(int id)
        {
            return _ctx.RigaDigitatas.Where(i => i.Id == id).ToListAsync();
        }

        public RigaDigitata getRiga()
        {
            var x =  _ctx.RigaDigitatas.Where(i => i.Id == 2).ToList();
            return x[0];
        }
    }
}
