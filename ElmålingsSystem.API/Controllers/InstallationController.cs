using ElmålingsSystem.API.Infrastructure;
using ElmålingsSystem.API.Models;
using ElmålingsSystem.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElmålingsSystem.API.Controllers
{
    [Route("api/Installation")]
    [ApiController]
    public class InstallationController : ControllerBase
    {
        private readonly IInstallationService _service;

        public InstallationController(IInstallationService service)
        {
            _service = service;
        }

        [HttpGet("{id}", Name = nameof(GetInstallation))]
        public async Task<ActionResult<InstallationDTO>> GetInstallation(int id)
        {
            var installation = await _service.GetInstallation(id);

            if (installation == null) return NotFound();

            return Ok(installation);
        }

        [HttpGet("ejerkunde/{ejerKundeId}", Name = nameof(GetAllInstallationer))]
        public async Task<ActionResult<IEnumerable<InstallationDTO>>> GetAllInstallationer(int ejerKundeId)
        {
            var installationer = await _service.GetAllInstallationerFromEjerKunde(ejerKundeId);

            if (installationer == null) return NotFound();

            return Ok(installationer);
        }

        [HttpPost("{ejerKundeId}", Name = nameof(PostInstallation))]
        public async Task<ActionResult<InstallationDTO>> PostInstallation(int ejerKundeId, [FromBody] InstallationDTO installation)
        {
            var nyInstallation = await _service.PostInstallation(ejerKundeId, installation);

            if (nyInstallation == null) return NotFound();

            return Ok(nyInstallation);
        }

        [HttpPut("{id}",Name = nameof(PutInstallation))]
        public async Task<ActionResult<InstallationDTO>> PutInstallation(int id, [FromBody]InstallationDTO installation)
        {
            var editedInstallation = await _service.PutInstallation(id, installation);

            if (editedInstallation == null) return NotFound();

            return Ok(editedInstallation);
        }

        [HttpDelete("{id}",Name = nameof(DeleteInstallation))]
        public async Task<ActionResult<InstallationDTO>> DeleteInstallation(int id)
        {
            var installation = await _service.DeleteInstallation(id);

            if (installation == false) return NotFound();

            return Ok();
        }
    }
}