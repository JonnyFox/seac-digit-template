﻿using AutoMapper;
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

        public DocumentoController(SeacDigitTemplateContext context, DocumentoService documentoService, IMapper mapper)
        {
            _ctx = context;
            _documentoService = documentoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(_mapper.Map<List<DocumentoDto>>(await _documentoService.GetAll()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<DocumentoDto>(await _documentoService.GetByIdAsync(id)));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            _documentoService.DeleteByIdAsync(id);
            return NoContent();
        }
        [HttpPost("saveChanges")]
        public IActionResult SaveDocument([FromBody] Documento documento)
        {
            _documentoService.SaveDocument(documento);
            return Ok();
        }
    }
}