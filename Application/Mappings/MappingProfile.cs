using AutoMapper;
using Domain.Entities;
using Domain.Models.DoctorDTO;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DoctorCreateDTO, Doctor>();
                  //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                  //.ForMember("Name", (config) =>
                  //{
                  //    config.MapFrom(x => x.Name);
                  //}
             //);
                
        }
    }
}
