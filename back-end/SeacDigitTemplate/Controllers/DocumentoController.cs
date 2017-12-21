using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Dtos;
using SeacDigitTemplate.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Models;


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
        public async Task<IActionResult> Get() => Ok(_mapper.Map<List<DocumentoDto>>(await _documentoService.GetAll()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            Documento x = new Documento {
            };
            x.isGenerated = false;


            if (id == 0)
            {
                return Ok(_mapper.Map<DocumentoDto>(x));
            }
            else
            {
                return Ok(_mapper.Map<DocumentoDto>(await _documentoService.GetByIdAsync(id)));
            }
        }
}
}
