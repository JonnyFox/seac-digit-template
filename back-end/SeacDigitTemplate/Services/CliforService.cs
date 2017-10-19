﻿using Microsoft.EntityFrameworkCore;
using SeacDigitTemplate.Data;
using SeacDigitTemplate.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Services
{
    public class CliforService
    {
        SeacDigitTemplateContex _ctx;
        public CliforService(SeacDigitTemplateContex ctx)
        {
            _ctx = ctx;
        }

        public Task<List<Clifor>> GetAll()
        {
            return _ctx.Clifors.ToListAsync();
        }
    }
}
