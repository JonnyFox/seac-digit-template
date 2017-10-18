﻿using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Services
{
    public class ContoService
    {
        SeacDigitTemplateContex _ctx;
        public ContoService( SeacDigitTemplateContex ctx)
        {
            _ctx = ctx;
        }
        public Task<List<Conto>> GetAll()=>  _ctx.Contos.ToListAsync();
       
    }
}
