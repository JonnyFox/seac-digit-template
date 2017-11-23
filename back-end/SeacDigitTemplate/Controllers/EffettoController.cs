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
        private readonly EffettoDocumentoService _effettoDocumentoService;
        private readonly IMapper _mapper;

        public EffettoController(EffettoService effettoService, RigaDigitataService rigaDigitataService, DocumentoService documentoService,EffettoDocumentoService effettoDocumentoService, IMapper mapper)
        {
            _effettoDocumentoService = effettoDocumentoService;
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

        //[HttpGet("calculate/{id}")]
        //public async Task<IActionResult> GetEffettos(int id)
        //{
        //    var documento = await _documentoService.GetByIdAsync(id);
        //    var righe = await _rigaDigitataService.GetByDocumentoIdAsync(id);
        //    var originalEffects = await _effettoService.GetEffettosFromInputListAsync(documento, righe);
        //    var situazioneVoceIvas = _effettoService.GetSituazioneVoceIva(originalEffects.EffettoList);
        //    var situazioneContos = _effettoService.GetSituazioneConto(originalEffects.EffettoList);

        //    var effettoCalcoloDto = _mapper.Map<EffettoCalcoloDto>(originalEffects.EffettoList);
        //    effettoCalcoloDto.SituazioneContos = _mapper.Map<List<SituazioneContoDto>>(situazioneContos);
        //    effettoCalcoloDto.SituazioneVoceIvas = _mapper.Map<List<SituazioneVoceIvaDto>>(situazioneVoceIvas);
        //    effettoCalcoloDto.EffettoDocumentoList = _mapper.Map<List<EffettoDocumentoDto>>(originalEffects.EffettoDocumentoList);

        //    return Ok(effettoCalcoloDto);
        //}

        [HttpPost("calculatePost")]
        public async Task<IActionResult> GetEffettosFromRigaDigitatas([FromBody] Documento documento)
        {
            //ResultList originalEffects;
            
            var EffettoList = await _effettoService.GetEffettosFromInputListAsync(documento, documento.rigaDigitataList);
            var EffettoDocumentoList = await _effettoDocumentoService.GetDocumentoListFromInputListAsync(documento, documento.rigaDigitataList);
            var situazioneVoceIvas = _effettoService.GetSituazioneVoceIva(EffettoList);
            var situazioneContos = _effettoService.GetSituazioneConto(EffettoList);

            var effettoCalcoloDto = _mapper.Map<EffettoCalcoloDto>(EffettoList);
            effettoCalcoloDto.SituazioneContos = _mapper.Map<List<SituazioneContoDto>>(situazioneContos);
            effettoCalcoloDto.SituazioneVoceIvas = _mapper.Map<List<SituazioneVoceIvaDto>>(situazioneVoceIvas);
            effettoCalcoloDto.EffettoDocumentoList = _mapper.Map<List<EffettoDocumentoDto>>(EffettoDocumentoList);

            return Ok(effettoCalcoloDto);
        }
    }
}
