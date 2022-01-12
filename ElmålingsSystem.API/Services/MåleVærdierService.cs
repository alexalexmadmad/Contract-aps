using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ElmålingsSystem.API.Models;
using ElmålingsSystem.DAL;
using Microsoft.EntityFrameworkCore;

namespace ElmålingsSystem.API.Services
{
    public class MåleVærdierService : IMåleVærdierService
    {
        private readonly MålingContext _context;
        private readonly IMapper _mapper;

        public MåleVærdierService(MålingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MåleVærdierDTO>> GetAllMåleVærdier(int målerId, DateTime start, DateTime end)
        {
            var måleVærdier = await _context.Måleværdier
                .Where((m => m.Måler.Id.Equals(målerId) &&
                        m.AflæsningDato >= start &&
                        m.AflæsningDato < end))
                .ToListAsync();

            if (måleVærdier == null) return null;

            return _mapper.Map<List<MåleVærdierDTO>>(måleVærdier);
        }
    }
}
