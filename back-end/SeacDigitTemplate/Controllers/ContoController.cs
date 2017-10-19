using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Services;

namespace SeacDigitTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/conto")]
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
