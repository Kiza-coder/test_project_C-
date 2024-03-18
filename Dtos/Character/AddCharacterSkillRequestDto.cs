using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_project.Dtos.Character
{
    public class AddCharacterSkillRequestDto
    {
        public int CharacterId { get; set;}
        public int SkillId {get; set;}
    }
}