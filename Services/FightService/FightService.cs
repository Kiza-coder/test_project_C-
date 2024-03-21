using System.Security.Claims;


namespace test_project.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FightService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<AttackResultResponseDto>> WeaponAttack(WeaponAttackRequestDto request)
        {
            var response = new ServiceResponse<AttackResultResponseDto>();

            try
            {
                var attacker = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId && c.User!.Id == GetUserId());
                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                if (attacker is null || opponent is null || attacker.Weapon is null)
                {
                    throw new Exception("Something fishy is going on here .. ");
                }

                int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strenght));
                damage -= new Random().Next(opponent.Defense);

                if (damage > 0)
                {
                    opponent.HitPoints -= damage;
                }

                if (opponent.HitPoints <= 0)
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

            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<AttackResultResponseDto>> SkillAttack(SkillAttackRequestDto request)
        {
            var response = new ServiceResponse<AttackResultResponseDto>();

            try
            {
                var attacker = await _context.Characters
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId && c.User!.Id == GetUserId());
                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

    
                if (attacker is null || opponent is null)
                {
                    throw new Exception("Attacker or Opponent doesnt exist");
                }

                if (!attacker.Skills!.Any())
                {
                    throw new Exception( $"{attacker.Name} doesnt know any skills");
                }

                var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);

                if (skill is null)
                {
                    response.Success = false;
                    response.Message = $"{attacker.Name} doesnt know that skill";
                    return response;
                }

                int damage = skill.Damage + (new Random().Next(attacker.Intelligence));
                damage -= new Random().Next(opponent.Defense);

                if (damage > 0)
                {
                    opponent.HitPoints -= damage;
                }

                if (opponent.HitPoints <= 0)
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

            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}