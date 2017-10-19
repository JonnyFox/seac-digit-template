using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Models;
using System.Collections.Generic;
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
    }
}
