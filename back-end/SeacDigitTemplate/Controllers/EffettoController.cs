﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Services;
using System.Threading.Tasks;
using AutoMapper;
using SeacDigitTemplate.Dtos;

namespace SeacDigitTemplate.Controllers
{
    [Route("api/effetto")]
    public class EffettoController : Controller
    {
        private readonly EffettoService _effettoService;
        private readonly RigaDigitataService _rigaDigitataService;
        private readonly IMapper _mapper;

        public EffettoController(EffettoService effettoService, RigaDigitataService rigaDigitataService, IMapper mapper)
        {
            _effettoService = effettoService;
            _rigaDigitataService = rigaDigitataService;
            _mapper = mapper;
        }

        [HttpGet("rigaDigitata/{id}")]
        public async Task<IActionResult> GetEffetto(int id)
        {
            var rigaDigitata = await _rigaDigitataService.GetByIdAsync(id);

            return Ok(await _effettoService.GetEffettosFromRigaDigitataAsync(rigaDigitata));
        }

        [HttpGet("calculate/{id}")]
        public async Task<IActionResult> GetEffettos(int id)
        {
            var x = new List<object>();
            var rigaDigitatas = await _rigaDigitataService.GetByDocumentoIdAsync(id);

            var originalEffects = await _effettoService.GetEffettosFromRigaDigitatasAsync(rigaDigitatas);
            var situazioneIva = await _effettoService.CreateSituazioneVoceIvaAsync(originalEffects);
            var situazioneConto = await _effettoService.CreateSituazioneContoAsync(originalEffects);

            x.Add(_mapper.Map<EffettoCalcoloDto>(originalEffects));
            x.Add(situazioneIva);
            x.Add(situazioneConto);

            return Ok(x);
        }
    }
}
