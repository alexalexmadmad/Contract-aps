using ElmålingsSystem.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElmålingsSystem.API.Services
{
    public interface ILejerKundeService
    {
        Task<LejerKundeDTO> GetLejerKundeByCpr(int lejerKundeCprNr);
        Task<IEnumerable<LejerKundeDTO>> GetAllLejerKunder();
        Task<LejerKundeDTO> PostLejerKunde(int installationsId, [FromBody]LejerKundeDTO lejerKunde);
        Task<LejerKundeDTO> PutLejerKundeById(int lejerKundeId, LejerKundeDTO lejerKunde);
        Task<bool> DeleteLejerKundeByCpr(int lejerKundeCprNr);
    }
}
