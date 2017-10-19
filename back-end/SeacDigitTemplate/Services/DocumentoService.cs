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
        SeacDigitTemplateContex _ctx;
        public DocumentoService(SeacDigitTemplateContex ctx)
        {
            _ctx = ctx;
        }

        public Task<List<Documento>> GetAll()
        {
            return _ctx.Documentos.ToListAsync();
        }

        public Task<List<Documento>> GetById(int id)
        {
            return _ctx.Documentos.Where(i => i.Id == id).ToListAsync();
        }
    }
}
