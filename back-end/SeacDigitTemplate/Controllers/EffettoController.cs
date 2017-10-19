using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Services;

namespace SeacDigitTemplate.Controllers
{
    [Route("api/effetto")]
    public class EffettoController : Controller
    {
        private EffettoService _effettoService;
        public EffettoController(EffettoService effettoService)
        {
            this._effettoService = effettoService;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get1()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public string postJeko([FromBody]EffettoCalcolo effettoCalcolo)
        {
            Documento documento = effettoCalcolo.documento;
            RigaDigitata[] rigaDigitataArray = effettoCalcolo.RigaDigitataList;

            return this._effettoService.GetEffetti(documento,rigaDigitataArray);
        }
        
    }
}
