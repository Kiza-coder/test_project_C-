using System.Data.SqlTypes;

namespace test_project
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterResponseDto>();
            CreateMap<AddCharacterRequestDto, Character>();
            CreateMap<UpdateCharacterRequestDto, Character>();
            CreateMap<Weapon, GetWeaponResponseDto>();
            CreateMap<Skill, GetSkillResponseDto>();  
        }
    }
}