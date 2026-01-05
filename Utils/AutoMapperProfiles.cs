using AutoMapper;
using SF_API.DTOs.Fighter;
using SF_API.DTOs.FighterMove;
using SF_API.DTOs.FighterVersion;
using SF_API.DTOs.Game;
using SF_API.DTOs.Image;
using SF_API.Models;

namespace SF_API.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<CreateFighterDTO, Fighter>();
            CreateMap<CreateGameDTO, Game>();
            CreateMap<CreateFighterVersionDTO, FighterVersion>();
            CreateMap<UpdateFighterVersionDTO, FighterVersion>();
            CreateMap<CreateFighterMoveDTO, FighterMove>();
            CreateMap<UpdateFighterMoveDTO, FighterMove>();
            CreateMap<CreateImageDTO, Image>();
        

        }

    }
}
