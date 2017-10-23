using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Services
{
    public class SituazioneContoService
    {
        SeacDigitTemplateContex _ctx;
        public SituazioneContoService(SeacDigitTemplateContex ctx)
        {
            _ctx = ctx;
        }

        public Task<List<SituazioneConto>> GetAll()
        {
            return _ctx.SituazioneContos.ToListAsync();
        }
        public Task<List<SituazioneConto>> GetById(int id)
        {
            return _ctx.SituazioneContos.Where(i => i.ContoId == id).ToListAsync();
        }
    }
}
