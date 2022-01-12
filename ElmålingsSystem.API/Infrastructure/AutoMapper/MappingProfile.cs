using AutoMapper;
using ElmålingsSystem.API.Models;
using ElmålingsSystem.DAL.Entities;

namespace ElmålingsSystem.API.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EjerKunde, EjerKundeDTO>().ReverseMap();
            CreateMap<LejerKunde, LejerKundeDTO>().ReverseMap();
            CreateMap<Installation, InstallationDTO>().ReverseMap();
            CreateMap<Måler, MålerDTO>().ReverseMap();
            CreateMap<Måleværdier, MåleVærdierDTO>().ReverseMap();
        }
    }
}
