using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Dtos;
using SeacDigitTemplate.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/clifor")]
    public class CliforController : ControllerBase
    {
        private CliforService _cliforService;
        private readonly IMapper _mapper;

        public CliforController(CliforService cliforService, IMapper mapper)
        {
            _cliforService = cliforService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<List<CliforDto>>(await _cliforService.GetById(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<CliforDto>>(await _cliforService.GetAll()));
        }

    }
}
