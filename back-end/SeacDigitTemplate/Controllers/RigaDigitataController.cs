using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Dtos;
using SeacDigitTemplate.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RigaDigitataController : ControllerBase
    {
        private RigaDigitataService _rigaDigitataService;
        private ApplicazioneTemplateEffettoService _applicazioneTemplateEffettoService;
        private readonly IMapper _mapper;

        public RigaDigitataController(RigaDigitataService rigaDigitataService, IMapper mapper, ApplicazioneTemplateEffettoService applicazioneTemplateEffettoService)
        {
            _rigaDigitataService = rigaDigitataService;
            _mapper = mapper;
            _applicazioneTemplateEffettoService = applicazioneTemplateEffettoService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAsync()
        //{
        //    return Ok(_mapper.Map<List<RigaDigitataDto>>(await _rigaDigitataService.GetAll()));
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<List<RigaDigitataDto>>(await _rigaDigitataService.GetById(id)));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _applicazioneTemplateEffettoService.GetTemplate(_rigaDigitataService.getRiga()));
        }
    }
}
