using AutoMapper;
using AutoMapper.QueryableExtensions;
using ElmålingsSystem.API.Models;
using ElmålingsSystem.DAL;
using ElmålingsSystem.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElmålingsSystem.API.Services
{
    public class EjerKundeService : IEjerKundeService
    {
        private readonly MålingContext _context;
        private readonly IMapper _mapper;

        public EjerKundeService(MålingContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EjerKundeDTO> GetEjerKunde(int id)
        {
            var ejerKunde = await _context.EjerKunder
                .SingleOrDefaultAsync(e => e.Id == id);

            if (ejerKunde == null) return null;

            return _mapper.Map<EjerKundeDTO>(ejerKunde);
        }

        public async Task<IEnumerable<EjerKundeDTO>> GetAllEjerKunder()
        {
            var ejerKunder = await _context.EjerKunder
                .ToListAsync();

            if (ejerKunder == null) return null;
            
            return _mapper.Map<List<EjerKundeDTO>>(ejerKunder);
        }


        public async Task<EjerKundeDTO> PostEjerKunde(EjerKundeDTO ejerKunde)
        {
            var nyEjerKunde = _mapper.Map<EjerKunde>(ejerKunde);

            _context.EjerKunder.Add(nyEjerKunde);
            await _context.SaveChangesAsync();

            return _mapper.Map<EjerKundeDTO>(nyEjerKunde);
        }

        public async Task<EjerKundeDTO> PutEjerKunde(int id, [FromBody]EjerKundeDTO ejerkunde)
        {
            var ejerKundeFromDb = await _context.EjerKunder
                .FirstOrDefaultAsync(e => e.Id == id);
            if (ejerKundeFromDb == null) return null;

            ejerKundeFromDb = _mapper.Map<EjerKunde>(ejerkunde);
            ejerKundeFromDb.Id = id;

            _context.Update(ejerKundeFromDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _mapper.Map<EjerKundeDTO>(ejerKundeFromDb);
        }

        public async Task<bool> DeleteEjerKunde(int id)
        {
            var ejerKunde = await _context.EjerKunder
                .SingleOrDefaultAsync(m => m.Id == id);

            if (ejerKunde == null) return false;

            _context.EjerKunder.Remove(ejerKunde);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
 