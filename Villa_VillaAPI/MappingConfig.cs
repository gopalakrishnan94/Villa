using System;
using AutoMapper;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO.VillaDTO;
using Villa_VillaAPI.Models.DTO.VillaNumberDTO;

namespace Villa_VillaAPI;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        // Villa
        CreateMap<Villa, VillaDTO>();
        CreateMap<VillaDTO, Villa>();

        CreateMap<Villa, CreateVillaDTO>().ReverseMap();
        CreateMap<Villa, UpdateVillaDTO>().ReverseMap();

        // VillaNumber
        CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
        CreateMap<VillaNumber, CreateVillaNumberDTO>().ReverseMap();
        CreateMap<VillaNumber, UpdateVillaNumberDTO>().ReverseMap();
    }
}
