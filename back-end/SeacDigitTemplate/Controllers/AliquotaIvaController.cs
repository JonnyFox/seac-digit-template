using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Services;

namespace SeacDigitTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/aliquotaIva")]
    public class AliquotaIvaController : ControllerBase
    {
        private AliquotaIvaService _aliquotaIvaService;

        public AliquotaIvaController(AliquotaIvaService aliquotaIvaService)
        {
            this._aliquotaIvaService = aliquotaIvaService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_aliquotaIvaService.GetAll());
        }
    }
}
