using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElmålingsSystem.API.Infrastructure;
using ElmålingsSystem.API.Models;
using ElmålingsSystem.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElmålingsSystem.API.Controllers
{
    [Route("api/LejerKunde")]
    [ApiController]
    public class LejerKundeController : ControllerBase
    {
        private readonly ILejerKundeService _service;

        public LejerKundeController(ILejerKundeService service)
        {
            _service = service;
        }

        [HttpGet("{lejerKundeCprNr}", Name = nameof(GetLejerKundeById))]
        public async Task<ActionResult<LejerKundeDTO>> GetLejerKundeById(int lejerKundeCprNr)
        {
            var lejerKunde = await _service.GetLejerKundeByCpr(lejerKundeCprNr);

            if (lejerKunde == null) return NotFound();

            return Ok(lejerKunde);
        }

        [HttpGet(Name = nameof(GetAllLejerKunder))]
        public async Task<ActionResult<IEnumerable<LejerKundeDTO>>> GetAllLejerKunder()
        {
            var lejerKunder = await _service.GetAllLejerKunder();

            return Ok(lejerKunder);
        }

        [HttpPost(Name = nameof(PostLejerKunde))]
        public async Task<ActionResult<LejerKundeDTO>> PostLejerKunde(int installationsId, [FromBody] LejerKundeDTO lejerKunde)
        {
            var nyLejerKunde = await _service.PostLejerKunde(installationsId, lejerKunde);

            if (nyLejerKunde == null) return NotFound();

            return Ok(nyLejerKunde);
        }

        [HttpPut("{lejerKundeId}",Name = nameof(PutLejerKundeById))]
        public async Task<ActionResult<LejerKundeDTO>> PutLejerKundeById(int lejerKundeId, LejerKundeDTO lejerKunde)
        {
            var editedLejerKunde = await _service.PutLejerKundeById(lejerKundeId, lejerKunde);

            if (editedLejerKunde == null) return NotFound();

            return Ok(editedLejerKunde);
        }

        [HttpDelete("{lejerKundeCprNr}",Name = nameof(DeleteLejerKundeById))]
        public async Task<ActionResult<LejerKundeDTO>> DeleteLejerKundeById(int lejerKundeCprNr)
        {
            var lejerKunde = await _service.DeleteLejerKundeByCpr(lejerKundeCprNr);

            if (lejerKunde == false) return NotFound();

            return Ok();
        }
    }
}