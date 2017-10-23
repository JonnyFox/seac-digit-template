using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Services
{
    public class TitoloInapplicabilitaService
    {
        SeacDigitTemplateContex _ctx;
        public TitoloInapplicabilitaService(SeacDigitTemplateContex ctx)
        {
            _ctx = ctx;
        }

        public Task<List<TitoloInapplicabilita>> GetAll()
        {
            return _ctx.TitoloInapplicabilitas.ToListAsync();
        }
        public Task<List<TitoloInapplicabilita>> GetById(int id)
        {
            return _ctx.TitoloInapplicabilitas.Where(i => i.Id == id).ToListAsync();
        }
    }
}
