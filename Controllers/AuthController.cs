using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace test_project.Controllers
{   
    [ApiController]
    [Route("[controller]")]
     public class AuthController: ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserRegisterRequestDto user)
        {
            var response = await  _authRepo.Register(
                new User {Name = user.Name},
                user.Password
            );
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

         [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginRequestDto user)
        {
            var response = await  _authRepo.Login(user.Name, user.Password);   
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}