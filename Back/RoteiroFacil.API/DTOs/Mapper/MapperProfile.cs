using AutoMapper;
using RoteiroFacil.API.Models;

namespace RoteiroFacil.API.DTOs.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UsuarioModel, UsuarioDTO>().ReverseMap();


        }
    }
}
