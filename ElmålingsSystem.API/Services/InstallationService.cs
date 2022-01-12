using AutoMapper;
using ElmålingsSystem.API.Models;
using ElmålingsSystem.DAL;
using ElmålingsSystem.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElmålingsSystem.API.Services
{
    public class InstallationService : IInstallationService
    {
        private readonly MålingContext _context;
        private readonly IMapper _mapper;

        public InstallationService(MålingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InstallationDTO> GetInstallation(int installationsId)
        {
            var installation = await _context.Installationer
                .SingleOrDefaultAsync(m => m.Id == installationsId);

            if (installation == null) return null;


            return  _mapper.Map<InstallationDTO>(installation);
        }

        public async Task<IEnumerable<InstallationDTO>> GetAllInstallationerFromEjerKunde(int kundeCprNr)
        {
            var installationer = await _context.Installationer
                .Where(i => i.EjerKunde.Id.Equals(kundeCprNr))
                .ToListAsync();

            if (!installationer.Any()) return null;

            return _mapper.Map<List<InstallationDTO>>(installationer);
        }

        public async Task<InstallationDTO> PostInstallation(int ejerKundeId, [FromBody] InstallationDTO installation)
        {
            var ejerKunde = await _context.EjerKunder
                .FirstOrDefaultAsync(e => e.Id == ejerKundeId);
            if (ejerKunde == null) return null;


            var nyInstallation = _mapper.Map<Installation>(installation);
            nyInstallation.EjerKundeId = ejerKunde.Id;


            _context.Installationer.Add(nyInstallation);
            await _context.SaveChangesAsync();

            var mappedInstallation = await GetInstallation(nyInstallation.Id);
            return mappedInstallation;
        }

        public async Task<InstallationDTO> PutInstallation(int id, [FromBody]InstallationDTO installation)
        {
            if (!_context.Installationer.Any(i=>i.Id == id)) return null;

            var editedInstallation = _mapper.Map<Installation>(installation);
            editedInstallation.Id = id;


            _context.Update(editedInstallation).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var mappedInstallation = await GetInstallation(editedInstallation.Id);
            return mappedInstallation;
        }

        public async Task<bool> DeleteInstallation(int installationsId)
        {
            var installation = await _context.Installationer.SingleOrDefaultAsync(m => m.Id == installationsId);
            if (installation == null) return false;

            _context.Installationer.Remove(installation);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
