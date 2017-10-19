using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Dtos;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SeacDigitTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/documento")]
    public class DocumentoController : Controller
    {
        private DocumentoService _documentoService;
        private readonly IMapper _mapper;
        
        public DocumentoController(DocumentoService documentoService, IMapper mapper)
        {
            _documentoService = documentoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<DocumentoDto>>(await _documentoService.GetAll()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_documentoService.GetById(id));
        }
    }
}
