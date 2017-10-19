using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SeacDigitTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/voceIva")]
    public class VoceIvaController : ControllerBase
    {
        private VoceIvaService _voceIvaService;

        public VoceIvaController(VoceIvaService voceIvaService)
        {
            this._voceIvaService = voceIvaService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_voceIvaService.GetAll());
        }
    }
}
