using System;
using AutoMapper;
using Villa_VillaWEB.Models.DTO.VillaDTO;
using Villa_VillaWEB.Models.DTO.VillaNumberDTO;

namespace Villa_VillaWEB;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        // Villa
        CreateMap<VillaDTO, CreateVillaDTO>().ReverseMap();
        CreateMap<VillaDTO, UpdateVillaDTO>().ReverseMap();

        // VillaNumber
        CreateMap<VillaNumberDTO, CreateVillaNumberDTO>().ReverseMap();
        CreateMap<VillaNumberDTO, UpdateVillaNumberDTO>().ReverseMap();
    }
}
