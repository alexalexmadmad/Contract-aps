using ElmålingsSystem.API.Infrastructure;
using ElmålingsSystem.API.Models;
using ElmålingsSystem.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ElmålingsSystem.API.Controllers
{
    [Route("api/EjerKunde")]
    [ApiController]
    public class EjerKundeController : ControllerBase
    {
        private readonly IEjerKundeService _service;

        public EjerKundeController(IEjerKundeService service)
        {
            _service = service;
        }

        // GET /api/EjerKunde/{ejerKundeCprNr}
        [HttpGet("/{id}", Name = nameof(GetEjerKunde))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<EjerKundeDTO>> GetEjerKunde(int id)
        {
            var ejerKunde = await _service.GetEjerKunde(id);

            if (ejerKunde == null) return NotFound();

            //return Ok(ejerKunde);
            return ejerKunde;
        }

        [HttpGet(Name = nameof(GetAllEjerKunder))]
        public async Task<ActionResult<IEnumerable<EjerKundeDTO>>> GetAllEjerKunder()
        {
            var ejerKunder = await _service.GetAllEjerKunder();
            return Ok(ejerKunder);
        }

        [HttpPost(Name = nameof(PostEjerKunde))]
        public async Task<ActionResult<EjerKundeDTO>> PostEjerKunde([FromBody] EjerKundeDTO ejerKunde)
        {
            var nyEjer = await _service.PostEjerKunde(ejerKunde);

            if (nyEjer == null) return NotFound();

            return Ok(nyEjer);
        }

        [HttpPut("{id}",Name = nameof(PutEjerKunde))]
        public async Task<ActionResult<EjerKundeDTO>> PutEjerKunde(int id, [FromBody]EjerKundeDTO ejerkunde)
        {
            var editedEjerKunde = await _service.PutEjerKunde(id, ejerkunde);

            if (editedEjerKunde == null) return NotFound();

            return Ok(editedEjerKunde);
        }

        [HttpDelete("{id}",Name = nameof(DeleteEjerKunde))]
        public async Task<IActionResult> DeleteEjerKunde(int id)
        {
            var ejer = await _service.DeleteEjerKunde(id);

            if (ejer == false) return NotFound();

            return Ok();
        }
    }
}