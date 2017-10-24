using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Services;
using AutoMapper;
using SeacDigitTemplate.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SeacDigitTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/voceIva")]
    public class VoceIvaController : ControllerBase
    {
        private VoceIvaService _voceIvaService;
        private readonly IMapper _mapper;

        public VoceIvaController(VoceIvaService voceIvaService, IMapper mapper)
        {
            _voceIvaService = voceIvaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<VoceIvaDto>>(await _voceIvaService.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<List<VoceIvaDto>>(await _voceIvaService.GetById(id)));
        }
    }
}
