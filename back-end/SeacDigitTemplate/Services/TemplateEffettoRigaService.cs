using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Services
{
    public class TemplateRigaService
    {
        SeacDigitTemplateContext _ctx;

        public TemplateRigaService(SeacDigitTemplateContext ctx)
        {
            _ctx = ctx;
        }

        public Task<List<TemplateRiga>> GetTemplateRigaAsync(ApplicazioneTemplateRiga appTemplateEffettoRiga)
        {
            return _ctx.TemplateEffettoRigaList.Where(te => te.ApplicazioneTemplateRigaId == appTemplateEffettoRiga.Id).ToListAsync();
        }
    }
}
