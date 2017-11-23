using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Services
{
    public class TemplateEffettoRigaService
    {
        SeacDigitTemplateContext _ctx;

        public TemplateEffettoRigaService(SeacDigitTemplateContext ctx)
        {
            _ctx = ctx;
        }

        public Task<List<TemplateEffettoRiga>> GetTemplateEffettoRigaAsync(ApplicazioneTemplateEffettoRiga appTemplateEffettoRiga)
        {
            return _ctx.TemplateEffettoRigaList.Where(te => te.ApplicazioneTemplateEffettoRigaId == appTemplateEffettoRiga.Id).ToListAsync();
        }
    }
}
