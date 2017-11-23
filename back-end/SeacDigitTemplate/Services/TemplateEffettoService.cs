using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Services
{
    public class TemplateEffettoService
    {
        SeacDigitTemplateContext _ctx;

        public TemplateEffettoService(SeacDigitTemplateContext ctx)
        {
            _ctx = ctx;
        }

        public Task<List<TemplateEffetto>> GetTemplateEffettoAsync(ApplicazioneTemplateEffetto appTemplateEffetto)
        {
            return _ctx.TemplateEffettoList.Where(te => te.ApplicazioneTemplateEffettoId == appTemplateEffetto.Id).ToListAsync();
        }
    }
}
