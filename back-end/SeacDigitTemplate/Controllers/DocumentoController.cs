using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Dtos;
using SeacDigitTemplate.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Models;
using SeacDigitTemplate.Data;
using Microsoft.EntityFrameworkCore;

namespace SeacDigitTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/documento")]
    public class DocumentoController : Controller
    {
        private DocumentoService _documentoService;
        private readonly IMapper _mapper;
        private SeacDigitTemplateContext _ctx;

        public DocumentoController(SeacDigitTemplateContext context,DocumentoService documentoService, IMapper mapper)
        {
            _ctx = context;
            _documentoService = documentoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(_mapper.Map<List<DocumentoDto>>(await _documentoService.GetAll()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {

            var lastDoc = _ctx.Documentos.LastAsync().Result;
            Documento x = new Documento {
                isGenerated = false,
                CliforId = 1,
                rigaDigitataList = new List<RigaDigitata>()
            };
            //if( lastDoc.isGenerated == null && id == 0)
            //{
            //    x.Id = lastDoc.Id;
            //    return Ok(_mapper.Map<DocumentoDto>(x));
            //}
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
