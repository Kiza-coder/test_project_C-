using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace test_project.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        

        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

    
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> Get() {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> GetSingle(int id) {

            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> AddCharacter(AddCharacterRequestDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter)
        {
            return Ok(await _characterService.UpdateCharacter(updatedCharacter));
        }
        

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> DeleteCharacter(int id)
        {
            return Ok(await _characterService.DeleteCharacter(id));
        }

        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> AddCharacterSkill(AddCharacterSkillRequestDto newCharacterSkill)
        {
            return Ok(await _characterService.AddCharacterSkill(newCharacterSkill));
        }




    }
