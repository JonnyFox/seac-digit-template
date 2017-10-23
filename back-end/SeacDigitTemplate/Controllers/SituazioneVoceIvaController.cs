using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Dtos;
using SeacDigitTemplate.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/situazioneVoceIva")]
    public class SituazioneVoceIvaController : ControllerBase
    {
        private SituazioneVoceIvaService _situazioneVoceIvaService;
        private readonly IMapper _mapper;

        public SituazioneVoceIvaController(SituazioneVoceIvaService situazioneVoceIvaService, IMapper mapper)
        {
            _situazioneVoceIvaService = situazioneVoceIvaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<SituazioneVoceIvaDto>>(await _situazioneVoceIvaService.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<List<SituazioneVoceIvaDto>>(await _situazioneVoceIvaService.GetById(id)));
        }
    }
}
