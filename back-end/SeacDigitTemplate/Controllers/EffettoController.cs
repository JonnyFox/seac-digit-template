using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Services;

namespace SeacDigitTemplate.Controllers
{
    [Route("api/effetto")]
    public class EffettoController : Controller
    {
        private EffettoCalcoloService _effettoService;
        public EffettoController(EffettoCalcoloService effettoService)
        {
            this._effettoService = effettoService;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get1()
        {
            return new string[] { "value1", "value2" };
        }

        
    }
}
