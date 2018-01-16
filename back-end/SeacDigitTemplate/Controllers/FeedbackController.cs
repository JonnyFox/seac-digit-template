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
    [Route("api/feedback")]
    public class FeedbackController : Controller
    {
        private FeedbackService _feedbackService;
        private readonly IMapper _mapper;
        private SeacDigitTemplateContext _ctx;

        public FeedbackController(SeacDigitTemplateContext context, FeedbackService feedbackService, IMapper mapper)
        {
            _ctx = context;
            _feedbackService = feedbackService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(_mapper.Map<List<FeedbackDto>>(await _feedbackService.GetAll()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<FeedbackDto>(await _feedbackService.GetByIdAsync(id)));
        }
    }
}