using AutoMapper;
using PeliculasAPI_Udemy.DTOs;
using PeliculasAPI_Udemy.Entidades;

namespace PeliculasAPI_Udemy
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreacionDTO, Actor>();
        }
    }
}
