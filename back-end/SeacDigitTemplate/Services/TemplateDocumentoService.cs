using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Services
{
    public class TemplateDocumentoService
    {
        SeacDigitTemplateContext _ctx;

        public TemplateDocumentoService(SeacDigitTemplateContext ctx)
        {
            _ctx = ctx;
        }

        public Task<List<TemplateDocumento>> GetTemplateEffettoAsync(ApplicazioneTemplateEffetto appTemplateEffetto)
        {
            return _ctx.TemplateDocumentos.Where(te => te.ApplicazioneTemplateEffettoId == appTemplateEffetto.Id).ToListAsync();
        }
    }
}
