using ElmålingsSystem.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElmålingsSystem.API.Services
{
    public interface IInstallationService
    {
        Task<InstallationDTO> GetInstallation(int id);
        Task<IEnumerable<InstallationDTO>> GetAllInstallationerFromEjerKunde(int ejerKundeId);
        Task<InstallationDTO> PostInstallation(int ejerKundeId, [FromBody] InstallationDTO installation);
        Task<InstallationDTO> PutInstallation(int Id, [FromBody] InstallationDTO installation);
        Task<bool> DeleteInstallation(int Id);
    }
}
