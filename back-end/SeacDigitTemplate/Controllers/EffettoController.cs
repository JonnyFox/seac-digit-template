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
        private readonly DocumentoService _documentoService;
        private readonly EffettoService _effettoService;
        private readonly RigaDigitataService _rigaDigitataService;
        private readonly IMapper _mapper;

        public EffettoController(EffettoService effettoService, RigaDigitataService rigaDigitataService, DocumentoService documentoService, IMapper mapper)
        {
            _documentoService = documentoService;
            _effettoService = effettoService;
            _rigaDigitataService = rigaDigitataService;
            _mapper = mapper;
        }

        [HttpGet("documento /{id}")]
        public async Task<IActionResult> GetEffetto(int id)
        {
            return Ok( await _documentoService.GetByIdAsync(id));
        }

        [HttpGet("calculate/{id}")]
        public async Task<IActionResult> GetEffettos(int id)
        {
            var documento = await _documentoService.GetByIdAsync(id);
            var righe = await _rigaDigitataService.GetByDocumentoIdAsync(id);
            var originalEffects = await _effettoService.GetEffettosFromInputListAsync(documento, righe);
            var situazioneVoceIvas = _effettoService.GetSituazioneVoceIva(originalEffects.EffettoRigaList);
            var situazioneContos = _effettoService.GetSituazioneConto(originalEffects.EffettoRigaList);

            var effettoCalcoloDto = _mapper.Map<EffettoCalcoloDto>(originalEffects.EffettoRigaList);
            effettoCalcoloDto.SituazioneContos = _mapper.Map<List<SituazioneContoDto>>(situazioneContos);
            effettoCalcoloDto.SituazioneVoceIvas = _mapper.Map<List<SituazioneVoceIvaDto>>(situazioneVoceIvas);
            effettoCalcoloDto.EffettoDocumentoList = _mapper.Map<List<EffettoDocumentoDto>>(originalEffects.EffettoDocumentoList);

            return Ok(effettoCalcoloDto);
        }

        [HttpPost("calculatePost")]
        public async Task<IActionResult> GetEffettosFromRigaDigitatas([FromBody] Documento documento)
        {

            var originalEffects = await _effettoService.GetEffettosFromInputListAsync(documento, documento.rigaDigitataList);
            var situazioneVoceIvas = _effettoService.GetSituazioneVoceIva(originalEffects.EffettoRigaList);
            var situazioneContos = _effettoService.GetSituazioneConto(originalEffects.EffettoRigaList);

            var effettoCalcoloDto = _mapper.Map<EffettoCalcoloDto>(originalEffects.EffettoRigaList);
            effettoCalcoloDto.SituazioneContos = _mapper.Map<List<SituazioneContoDto>>(situazioneContos);
            effettoCalcoloDto.SituazioneVoceIvas = _mapper.Map<List<SituazioneVoceIvaDto>>(situazioneVoceIvas);
            effettoCalcoloDto.EffettoDocumentoList = _mapper.Map<List<EffettoDocumentoDto>>(originalEffects.EffettoDocumentoList);

            return Ok(effettoCalcoloDto);
        }
    }
}
