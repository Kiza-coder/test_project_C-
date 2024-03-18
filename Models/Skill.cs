using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_project.Models
{
    public class Skill
    {
        public int Id{get; set;} 
        public string Name{get; set;} = string.Empty;
        public int Damamge {get; set;} 
        public List<Character>? Characters {get; set;} 
    }
}