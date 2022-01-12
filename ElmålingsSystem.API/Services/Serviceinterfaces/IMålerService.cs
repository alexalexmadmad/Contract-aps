using System.Threading.Tasks;
using ElmålingsSystem.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElmålingsSystem.API.Services
{
    public interface IMålerService
    {
        Task<MålerDTO> GetMålerByInstallationsId(int installationsId);
        Task<MålerDTO> PostMåler(int installationsId, [FromBody] MålerDTO måler);
        Task<MålerDTO> PutMålerById(int målerId, [FromBody] MålerDTO måler);
        Task<bool> DeleteMålerById(int målerId);
    }
}