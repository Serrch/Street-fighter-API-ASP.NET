using AutoMapper;
using SF_API.DTOs.Fighter;
using SF_API.DTOs.Game;
using SF_API.Models;

namespace SF_API.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<CreateFighterDTO, Fighter>();
            CreateMap<CreateGameDTO, Game>();

        

        }

    }
}
