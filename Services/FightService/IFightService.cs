using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test_project.Dtos.Fight;

namespace test_project.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultResponseDto>> WeaponAttack(WeaponAttackRequestDto request);
        Task<ServiceResponse<AttackResultResponseDto>> SkillAttack(SkillAttackRequestDto request);

    }
}