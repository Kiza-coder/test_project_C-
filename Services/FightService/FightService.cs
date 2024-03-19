using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test_project.Dtos.Fight;

namespace test_project.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
       
        public FightService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<AttackResultResponseDto>> WeaponAttack(WeaponAttackRequestDto request)
        {
            var response = new ServiceResponse<AttackResultResponseDto>();

            try
            {
                var attacker = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);
                
                if(attacker is null || opponent is null || attacker.Weapon is null)
                {
                    throw new Exception("Something fishy is going on here .. ");
                }

                int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strenght));
                damage -= new Random().Next(opponent.Defense);

                if(damage > 0)
                {
                    opponent.HitPoints -= damage;
                }

                if(opponent.HitPoints <= 0)
                {
                    response.Message = $"{opponent.Name} has been defeated !";
                }
                
                await _context.SaveChangesAsync();
                response.Data = new AttackResultResponseDto
                {
                    Attacker = attacker.Name,
                    AttackerHP = attacker.HitPoints,
                    Opponent = opponent.Name,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };

            }

            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}