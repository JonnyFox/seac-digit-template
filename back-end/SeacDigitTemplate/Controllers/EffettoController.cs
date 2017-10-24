using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SeacDigitTemplate.Model;
using SeacDigitTemplate.Services;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Controllers
{
    [Route("api/effetto")]
    public class EffettoController : Controller
    {
        private readonly EffettoService _effettoService;
        private readonly RigaDigitataService _rigaDigitataService;

        public EffettoController(EffettoService effettoService, RigaDigitataService rigaDigitataService)
        {
            _effettoService = effettoService;
            _rigaDigitataService = rigaDigitataService;
        }

        [HttpGet("rigaDigitata/{id}")]
        public async Task<IActionResult> GetEffetto(int id)
        {
            var rigaDigitata = await _rigaDigitataService.GetByIdAsync(id);

            return Ok(await _effettoService.GetEffettosFromRigaDigitataAsync(rigaDigitata));
        }

        [HttpGet("calculate")]
        public async Task<IActionResult> GetEffettos()
        {
            var rigaDigitatas = new List<RigaDigitata>
            {
                new RigaDigitata{
                    DocumentoId = 1,
                    ContoDareId = 1,
                    ContoAvereId = 10,
                    VoceIvaId = 1,
                    AliquotaIvaId = 3,
                    Trattamento = TrattamentoEnum.Detraibile,
                    TitoloInapplicabilita = null,
                    Imponibile = 1000.0m,
                    Iva = 220.0m,
                    PercentualeIndeducibilita = null,
                    PercentualeIndetraibilita = null,
                    Settore = null,
                    Note = null },
                new RigaDigitata{
                    DocumentoId = 1,
                    ContoDareId = 2,
                    ContoAvereId = 10,
                    VoceIvaId = 1,
                    AliquotaIvaId = 2,
                    Trattamento = TrattamentoEnum.Detraibile,
                    TitoloInapplicabilita = null ,
                    Imponibile = 1000.0m,
                    Iva = 100,
                    PercentualeIndeducibilita = null,
                    PercentualeIndetraibilita = null,
                    Settore = null,
                    Note = null},
                new RigaDigitata{
                    DocumentoId = 1,
                    ContoDareId = 3,
                    ContoAvereId = 10,
                    VoceIvaId = 1,
                    AliquotaIva = null,
                    Trattamento = null,
                    TitoloInapplicabilitaId = 3,
                    Imponibile = 2.0m,
                    Iva = null ,
                    PercentualeIndeducibilita = null,
                    PercentualeIndetraibilita = null,
                    Settore = null,
                    Note = null },
            };


            return Ok(await _effettoService.GetEffettosFromRigaDigitatasAsync(rigaDigitatas));
        }
    }
}
