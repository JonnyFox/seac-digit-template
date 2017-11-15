using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Dtos;
using SeacDigitTemplate.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/aliquotaIva")]
    public class AliquotaIvaController : ControllerBase
    {
        private AliquotaIvaService _aliquotaIvaService;
        private readonly IMapper _mapper;

        public AliquotaIvaController(AliquotaIvaService aliquotaIvaService, IMapper mapper)
        {
            _aliquotaIvaService = aliquotaIvaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<AliquotaIvaDto>>(await _aliquotaIvaService.GetAll()));
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<List<AliquotaIvaDto>>(await _aliquotaIvaService.GetById(id)));
        }
    }
}
