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
    public class MålerService : IMålerService
    {
        private readonly MålingContext _context;
        private readonly IConfigurationProvider _mappingConfiguration;

        public MålerService(MålingContext context, IConfigurationProvider mappingConfiguration)
        {
            _context = context;
            _mappingConfiguration = mappingConfiguration;
        }

        public async Task<MålerDTO> GetMålerByInstallationsId(int installationsId)
        {
            //search in Måler(db), if there's a 'Måler' with a MålerId, equal to målerId
            var måler = await _context.Måler.SingleOrDefaultAsync(m => m.Installation.Id == installationsId);

            if (måler == null) return null;

            //returns a mapped MålerVM, with attributes from Måler
            var mapper = _mappingConfiguration.CreateMapper();

            return mapper.Map<MålerDTO>(måler);
        }

        public async Task<MålerDTO> PostMåler(int installationsId, [FromBody] MålerDTO måler)
        {
            var installation = await _context.Installationer.FirstOrDefaultAsync(i => i.Id == installationsId);
            if (installation == null) return null;

            var mapper = _mappingConfiguration.CreateMapper();

            var nyMåler = mapper.Map<Måler>(måler);
            nyMåler.InstallationId = installation.Id;

            _context.Måler.Add(nyMåler);
            await _context.SaveChangesAsync();

            var mappedMåler = await GetMålerByInstallationsId(nyMåler.Id);

            return mappedMåler;
        }

        public async Task<MålerDTO> PutMålerById(int målerId, [FromBody] MålerDTO måler)
        {
            var installation = await _context.Installationer.SingleOrDefaultAsync(k => k.Måler.Id.Equals(målerId));
            if (installation == null) return null;

            var mapper = _mappingConfiguration.CreateMapper();
            var editedMåler = mapper.Map<Måler>(måler);

            editedMåler.InstallationId = installation.Id;
            editedMåler.Id = målerId;

            _context.Update(editedMåler).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var mappedMåler = await GetMålerByInstallationsId(editedMåler.Id);
            return mappedMåler;
        }     

        public async Task<bool> DeleteMålerById(int målerId)
        {
            var måler = await _context.Måler.SingleOrDefaultAsync(m => m.Id == målerId);
            if (måler == null) return false;

            _context.Måler.Remove(måler);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
