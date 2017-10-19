using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Services;
using System.Collections.Generic;

namespace SeacDigitTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/situazioneVoceIva")]
    public class SituazioneVoceIvaController : ControllerBase
    {
        private SituazioneVoceIvaService _situazioneVoceIvaService;

        public SituazioneVoceIvaController(SituazioneVoceIvaService situazioneVoceIvaService)
        {
            this._situazioneVoceIvaService = situazioneVoceIvaService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_situazioneVoceIvaService.GetAll());
        }
    }
}
