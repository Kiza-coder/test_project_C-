

using System.Security.Claims;

namespace test_project.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WeaponService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

                private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<GetCharacterResponseDto>> AddWeapon(AddWeaponRequestDto newWeapon)
        {
           var response = new ServiceResponse<GetCharacterResponseDto>();

           try
           {   
                var character = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId &&
                    c.User!.Id == GetUserId());
                
                if(character is null)
                {
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }

                var weapon = new Weapon {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    Character = character,
                };

                _context.Weapons.Add(weapon);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterResponseDto>(character);

                    
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