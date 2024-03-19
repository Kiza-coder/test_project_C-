using Microsoft.AspNetCore.Mvc;


namespace test_project.Controllers
{
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
    }
}