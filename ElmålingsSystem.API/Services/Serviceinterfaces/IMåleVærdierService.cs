using ElmålingsSystem.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElmålingsSystem.API.Services
{
    public interface IMåleVærdierService
    {
        Task<IEnumerable<MåleVærdierDTO>> GetAllMåleVærdier(int målerId, DateTime start, DateTime end);
    }
}
