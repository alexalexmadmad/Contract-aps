using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class LejerKundeService : ILejerKundeService
    {
        private readonly MålingContext _context;
        private readonly IConfigurationProvider _mappingConfiguration;

        public LejerKundeService(MålingContext context, IConfigurationProvider mappingConfiguration)
        {
            _context = context;
            _mappingConfiguration = mappingConfiguration;
        }

        public async Task<LejerKundeDTO> GetLejerKundeByCpr(int lejerKundeCprNr)
        {
            //search in LejerKunder(db), if there's a 'Lejerkunde' with a KundeId, equal to lejerKundeId
            var lejerkunde = await _context.LejerKunder.SingleOrDefaultAsync(m => m.CprNr == lejerKundeCprNr);

            if (lejerkunde == null) return null;

            //returns a mapped LejerKundeVM, with attributes from LejerKunde
            var mapper = _mappingConfiguration.CreateMapper();
            return mapper.Map<LejerKundeDTO>(lejerkunde);
        }

        public async Task<IEnumerable<LejerKundeDTO>> GetAllLejerKunder()
        {
            var lejerKunder = _context.LejerKunder.ProjectTo<LejerKundeDTO>(_mappingConfiguration);

            //return _ignore.GetAllModelsWtihIgnoredNullValues(ejerKunder);
            return await lejerKunder.ToArrayAsync();
        }

        public async Task<LejerKundeDTO> PostLejerKunde(int installationsId, [FromBody] LejerKundeDTO lejerKunde)
        {
            var installation = await _context.Installationer.FirstOrDefaultAsync(i => i.Id == installationsId);
            if (installation == null) return null;

            var mapper = _mappingConfiguration.CreateMapper();

            var nyLejerKunde = mapper.Map<LejerKunde>(lejerKunde);
            nyLejerKunde.InstallationId = installation.Id;

            _context.LejerKunder.Add(nyLejerKunde);
            await _context.SaveChangesAsync();

            var mappedLejerKunde = await GetLejerKundeByCpr(nyLejerKunde.CprNr);

            return mappedLejerKunde;
        }

        public async Task<LejerKundeDTO> PutLejerKundeById(int lejerKundeId, [FromBody]LejerKundeDTO lejerKunde)
        {
            //var installation = await _context.Installationer.SingleOrDefaultAsync(k => k.InstallationsId == installationsId);
            var installation = await _context.Installationer.FirstOrDefaultAsync(k => k.LejerKunde.Id.Equals(lejerKundeId));
            if (installation == null) return null;

            var mapper = _mappingConfiguration.CreateMapper();
            var editedLejerKunde = mapper.Map<LejerKunde>(lejerKunde);

            editedLejerKunde.InstallationId = installation.Id;
            editedLejerKunde.Id = lejerKundeId;

            _context.Update(editedLejerKunde).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var mappedLejerKunde = await GetLejerKundeByCpr(editedLejerKunde.CprNr);
            return mappedLejerKunde;
        }

        public async Task<bool> DeleteLejerKundeByCpr(int lejerKundeCprNr)
        {
            var lejerKunde = await _context.LejerKunder.SingleOrDefaultAsync(m => m.CprNr == lejerKundeCprNr);
            if (lejerKunde == null) return false;

            _context.LejerKunder.Remove(lejerKunde);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
