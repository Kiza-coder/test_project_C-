using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace test_project.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FightController : ControllerBase
    {
        private readonly IFightService _fightService;
        public FightController(IFightService fightService)
        {
            _fightService = fightService;
        }

        [HttpPost("Weapon")]
        public async Task<ActionResult<ServiceResponse<AttackResultResponseDto>>> WeaponAttack(WeaponAttackRequestDto request)
        {
            return Ok(await _fightService.WeaponAttack(request));
        }

        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<AttackResultResponseDto>>> SkillAttack(SkillAttackRequestDto request)
        {
            return Ok(await _fightService.SkillAttack(request));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<FightResultResponseDto>>> Fight(FightRequestDto request)
        {
            return Ok(await _fightService.Fight(request));
        }
    }
}