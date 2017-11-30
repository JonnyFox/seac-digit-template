using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Services;
using System.Threading.Tasks;
using AutoMapper;
using SeacDigitTemplate.Dtos;
using SeacDigitTemplate.Data;

namespace SeacDigitTemplate.Controllers
{
    [Route("api/effetto")]
    public class EffettoController : Controller
    {
        private readonly DocumentoService _documentoService;
        private readonly EffettoService _effettoService;
        private readonly RigaDigitataService _rigaDigitataService;
        private readonly EffettoDocumentoService _effettoDocumentoService;
        private readonly EffettoRigaService _effettoRigaService;
        private readonly IMapper _mapper;
        private SeacDigitTemplateContext _ctx;

        public EffettoController(SeacDigitTemplateContext context,EffettoService effettoService, RigaDigitataService rigaDigitataService, DocumentoService documentoService,EffettoDocumentoService effettoDocumentoService,EffettoRigaService effettoRigaService, IMapper mapper)
        {
            _ctx = context;
            _effettoRigaService = effettoRigaService;
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

        [HttpGet("calculate/{id}")]
        public async Task<IActionResult> GetEffettos(int id)
        {
            var documento = await _documentoService.GetByIdAsync(id);
            var righe = await _rigaDigitataService.GetByDocumentoIdAsync(id);
            var EffettoList = await _effettoService.GetEffettosFromInputListAsync(documento, righe);
            var situazioneVoceIvas = _effettoService.GetSituazioneVoceIva(EffettoList);
            var situazioneContos = _effettoService.GetSituazioneConto(EffettoList);
            var EffettoDocumentoList = await _effettoDocumentoService.GetDocumentoListFromInputListAsync(documento, documento.rigaDigitataList);
            var EffettoRigaList = await _effettoRigaService.GetEffettoRigaListFromInputListAsync(documento, documento.rigaDigitataList, EffettoDocumentoList);

            var effettoCalcoloDto = _mapper.Map<EffettoCalcoloDto>(EffettoList);
            effettoCalcoloDto.SituazioneContos = _mapper.Map<List<SituazioneContoDto>>(situazioneContos);
            effettoCalcoloDto.SituazioneVoceIvas = _mapper.Map<List<SituazioneVoceIvaDto>>(situazioneVoceIvas);
            effettoCalcoloDto.EffettoDocumentoList = _mapper.Map<List<DocumentoDto>>(EffettoDocumentoList);
            effettoCalcoloDto.EffettoRigaList = _mapper.Map<List<RigaDigitataDto>>(EffettoRigaList);

            return Ok(effettoCalcoloDto);
        }

        [HttpPost("calculatePost")]
        public async Task<IActionResult> GetEffettosFromRigaDigitatas([FromBody] Documento documento)
        {

            var EffettoList = await _effettoService.GetEffettosFromInputListAsync(documento, documento.rigaDigitataList);
            var situazioneVoceIvas = _effettoService.GetSituazioneVoceIva(EffettoList);
            var situazioneContos = _effettoService.GetSituazioneConto(EffettoList);
            var EffettoDocumentoList = await _effettoDocumentoService.GetDocumentoListFromInputListAsync(documento, documento.rigaDigitataList);
            var EffettoRigaList = await _effettoRigaService.GetEffettoRigaListFromInputListAsync(documento, documento.rigaDigitataList, EffettoDocumentoList);


            var effettoCalcoloDto = _mapper.Map<EffettoCalcoloDto>(EffettoList);
            effettoCalcoloDto.SituazioneContos = _mapper.Map<List<SituazioneContoDto>>(situazioneContos);
            effettoCalcoloDto.SituazioneVoceIvas = _mapper.Map<List<SituazioneVoceIvaDto>>(situazioneVoceIvas);
            effettoCalcoloDto.EffettoDocumentoList = _mapper.Map<List<DocumentoDto>>(EffettoDocumentoList);
            effettoCalcoloDto.EffettoRigaList = _mapper.Map<List<RigaDigitataDto>>(EffettoRigaList);

            return Ok(effettoCalcoloDto);
        }
        [HttpPost("sendFeedback")]
        public IActionResult SendFeedback([FromBody] Feedback feedback)
        {
            _ctx.FeedbackList.Add(feedback);
            _ctx.SaveChanges();
            return Ok();
        }
    }
}
