using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElmålingsSystem.API.Infrastructure;
using ElmålingsSystem.API.Services;
using ElmålingsSystem.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElmålingsSystem.API.Controllers
{
    [Route("api/Måleværdier")]
    [ApiController]
    public class MåleVærdierController : ControllerBase
    {
        private readonly IMåleVærdierService _service;

        public MåleVærdierController(IMåleVærdierService service)
        {
            _service = service;
        }

        [HttpGet("{målerId}", Name = nameof(GetAllMåleVærdier))]
        public async Task<ActionResult<IEnumerable<MåleVærdierDTO>>> GetAllMåleVærdier(int målerId, int start = 10, int end = 35)
        {
            var måleVærdier = await _service.GetAllMåleVærdier(målerId, DateTime.Now.AddDays(start), DateTime.Now.AddDays(end));

            if (måleVærdier == null) return NotFound();

            return Ok(måleVærdier);
        }
    }
}