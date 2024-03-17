using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_project.Dtos.User
{
    public class UserRegisterRequestDto
    {
        public string Name { get; set;} = string.Empty;
        public string Password {get; set;} = string.Empty;

    }
}