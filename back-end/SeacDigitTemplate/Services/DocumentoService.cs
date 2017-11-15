using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Services
{
    public class DocumentoService
    {
        SeacDigitTemplateContext _ctx;
        public DocumentoService(SeacDigitTemplateContext ctx) => _ctx = ctx;

        public Task<List<Documento>> GetAll() => _ctx.Documentos.ToListAsync();

        public Task<Documento> GetByIdAsync(int id) => _ctx.Documentos.SingleOrDefaultAsync(i => i.Id == id);
    }
}
