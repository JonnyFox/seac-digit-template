using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Services;
using SeacDigitTemplate.Model;

namespace SeacDigitTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/Conto")]
    public class ContoController : ControllerBase
    {
        private ContoService _contoService;
        public ContoController(ContoService contoService)
        {
            this._contoService = contoService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_contoService.GetAll());
        }

    }
}
