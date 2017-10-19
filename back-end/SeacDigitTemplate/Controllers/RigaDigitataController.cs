using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Services;

namespace SeacDigitTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RigaDigitataController : ControllerBase
    {
        private RigaDigitataService _rigaDigitataService;

        public RigaDigitataController(RigaDigitataService rigaDigitataService)
        {
            this._rigaDigitataService = rigaDigitataService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_rigaDigitataService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_rigaDigitataService.GetById(id));
        }
    }
}
