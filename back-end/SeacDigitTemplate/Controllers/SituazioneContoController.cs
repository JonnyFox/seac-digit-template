using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Dtos;
using SeacDigitTemplate.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/situazioneConto")]
    public class SituazioneContoController : ControllerBase
    {
        private SituazioneContoService _situazioneContoService;
        private readonly IMapper _mapper;

        public SituazioneContoController(SituazioneContoService situazioneContoService, IMapper mapper)
        {
            _situazioneContoService = situazioneContoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<SituazioneContoDto>>(await _situazioneContoService.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<List<SituazioneContoDto>>(await _situazioneContoService.GetById(id)));
        }
    }
}
