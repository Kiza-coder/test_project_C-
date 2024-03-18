using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_project.Dtos.Skill
{
    public class GetSkillResponseDto
    {
        public string Name {get;set;} = string.Empty;
        public int Damage {get;set;}
    }
}