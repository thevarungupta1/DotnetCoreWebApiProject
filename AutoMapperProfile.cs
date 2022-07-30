using AutoMapper;
using DotnetCoreWebApiProject.Dtos;
using DotnetCoreWebApiProject.Models;

namespace DotnetCoreWebApiProject
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();
                
        }
    }
}
