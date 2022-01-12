using ElmålingsSystem.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElmålingsSystem.API.Services
{
    public interface IEjerKundeService
    {
        Task<EjerKundeDTO> GetEjerKunde(int id);
        Task<IEnumerable<EjerKundeDTO>> GetAllEjerKunder();
        Task<EjerKundeDTO> PostEjerKunde([FromBody] EjerKundeDTO ejerKunde);
        Task<EjerKundeDTO> PutEjerKunde(int id, [FromBody] EjerKundeDTO ejerKunde);
        Task<bool> DeleteEjerKunde(int id);
    }
}
