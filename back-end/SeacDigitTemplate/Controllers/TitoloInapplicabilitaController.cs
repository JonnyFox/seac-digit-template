using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Services;
using AutoMapper;
using SeacDigitTemplate.Dtos;

namespace SeacDigitTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/titoloInapplicabilita")]

    public class TitoloInapplicabilitaController : ControllerBase
    {
        private TitoloInapplicabilitaService _titoloInapplicabilitaService;
        private readonly IMapper _mapper;

        public TitoloInapplicabilitaController(TitoloInapplicabilitaService titoloInapplicabilitaService, IMapper mapper)
        {
            _titoloInapplicabilitaService = titoloInapplicabilitaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<TitoloInapplicabilitaDto>>(await _titoloInapplicabilitaService.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<List<TitoloInapplicabilitaDto>>(await _titoloInapplicabilitaService.GetById(id)));
        }
    }
}
