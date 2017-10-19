using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Services;

namespace SeacDigitTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/clifor")]
    public class CliforController : ControllerBase
    {
        private CliforService _cliforService;

        public CliforController(CliforService cliforService)
        {
            this._cliforService = cliforService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_cliforService.GetAll());
        }
    }
}
