using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Dtos;
using SeacDigitTemplate.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/conto")]
    public class ContoController : ControllerBase
    {
        private ContoService _contoService;
        private readonly IMapper _mapper;
        public ContoController(ContoService contoService, IMapper mapper)
        {
            _contoService = contoService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<List<ContoDto>>(await _contoService.GetById(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<ContoDto>>(await _contoService.GetAll()));
        }

    }
}
