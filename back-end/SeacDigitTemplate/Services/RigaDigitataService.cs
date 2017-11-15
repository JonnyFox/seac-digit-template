using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace SeacDigitTemplate.Services
{
    public class RigaDigitataService
    {
        SeacDigitTemplateContext _ctx;
        public RigaDigitataService(SeacDigitTemplateContext ctx)
        {
            _ctx = ctx;
        }

        public Task<List<RigaDigitata>> GetAll() => _ctx.RigaDigitatas.ToListAsync();
        public Task<RigaDigitata> GetByIdAsync(int id) => _ctx.RigaDigitatas.SingleOrDefaultAsync(i => i.Id == id);

        public Task<List<RigaDigitata>> GetByDocumentoIdAsync(int id) => _ctx.RigaDigitatas.Where(rd => rd.DocumentoId == id).ToListAsync();
    }
}
