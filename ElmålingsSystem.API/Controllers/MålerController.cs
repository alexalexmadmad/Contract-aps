using System.Threading.Tasks;
using ElmålingsSystem.API.Models;
using ElmålingsSystem.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElmålingsSystem.API.Controllers
{
    [Route("api/Måler")]
    [ApiController]
    public class MålerController : ControllerBase
    {
        private readonly IMålerService _service;

        public MålerController(IMålerService service)
        {
            _service = service;
        }

        [HttpGet("{installationsId}",Name = nameof(GetMålerByInstallationsId))]
        public async Task<ActionResult<MålerDTO>> GetMålerByInstallationsId(int installationsId)
        {
            var måler = await _service.GetMålerByInstallationsId(installationsId);

            if (måler == null) return NotFound();

            return Ok(måler);
        }

        [HttpPost(Name = nameof(PostMåler))]
        public async Task<ActionResult<MålerDTO>> PostMåler(int installationsId, [FromBody] MålerDTO måler)
        {
            var nyMåler = await _service.PostMåler(installationsId, måler);

            if (nyMåler == null) return NotFound();

            return Ok(nyMåler);
        }

        [HttpPut("{målerId}", Name = nameof(PutMålerById))]
        public async Task<ActionResult<MålerDTO>> PutMålerById(int målerId, [FromBody] MålerDTO måler)
        {
            var editedMåler = await _service.PutMålerById(målerId, måler);

            if (editedMåler == null) return NotFound();

            return Ok(editedMåler);
        }

        [HttpDelete("{målerId}", Name = nameof(DeleteMålerById))]
        public async Task<ActionResult<MålerDTO>> DeleteMålerById(int målerId)
        {
            var måler = await _service.DeleteMålerById(målerId);

            if (måler == false) return NotFound();

            return Ok();
        }
    }
}