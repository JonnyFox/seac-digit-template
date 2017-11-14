using System.Collections.Generic;
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

        //[HttpGet("rigaDigitata/{id}")]
        //public async Task<IActionResult> GetEffetto(int id)
        //{
        //    var rigaDigitata = await _rigaDigitataService.GetByIdAsync(id);

        //    return Ok(await _effettoService.GetEffettosFromRigaDigitataAsync(rigaDigitata));
        //}

        //[HttpGet("calculate/{id}")]
        //public async Task<IActionResult> GetEffettos(int id)
        //{
        //    var rigaDigitatas = await _rigaDigitataService.GetByDocumentoIdAsync(id);

        //    var originalEffects = await _effettoService.GetEffettosFromRigaDigitatasAsync(rigaDigitatas);
        //    var situazioneVoceIvas =  _effettoService.GetSituazioneVoceIva(originalEffects);
        //    var situazioneContos =  _effettoService.GetSituazioneConto(originalEffects);

        //    var effettoCalcoloDto = _mapper.Map<EffettoCalcoloDto>(originalEffects);
        //    effettoCalcoloDto.SituazioneContos = _mapper.Map<List<SituazioneContoDto>>(situazioneContos);
        //    effettoCalcoloDto.SituazioneVoceIvas = _mapper.Map<List<SituazioneVoceIvaDto>>(situazioneVoceIvas);

        //    return Ok(effettoCalcoloDto);
        //}

        [HttpPost("calculatePost")]
        public async Task<IActionResult> GetEffettosFromRigaDigitatas([FromBody] Documento documento)
        {

            var originalEffects = await _effettoService.GetEffettosFromRigaDigitatasAsync(documento, documento.rigaDigitataList);
            var situazioneVoceIvas = _effettoService.GetSituazioneVoceIva(originalEffects);
            var situazioneContos = _effettoService.GetSituazioneConto(originalEffects);

            var effettoCalcoloDto = _mapper.Map<EffettoCalcoloDto>(originalEffects);
            effettoCalcoloDto.SituazioneContos = _mapper.Map<List<SituazioneContoDto>>(situazioneContos);
            effettoCalcoloDto.SituazioneVoceIvas = _mapper.Map<List<SituazioneVoceIvaDto>>(situazioneVoceIvas);

            return Ok(effettoCalcoloDto);
        }
    }
}
