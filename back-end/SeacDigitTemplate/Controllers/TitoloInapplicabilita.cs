using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Services;

namespace SeacDigitTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/titoloInapplicabilita")]

    public class TitoloInapplicabilita : ControllerBase
    {
        private TitoloInapplicabilitaService _titoloInapplicabilitaService;

        public TitoloInapplicabilita(TitoloInapplicabilitaService titoloInapplicabilitaService)
        {
            this._titoloInapplicabilitaService = titoloInapplicabilitaService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_titoloInapplicabilitaService.GetAll());
        }
    }
}
