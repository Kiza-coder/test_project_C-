
using System.Security.Claims;

namespace test_project.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        
        
        
        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharacter(AddCharacterRequestDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Characters
                .Where(c => c.User!.Id == GetUserId() )
                .Select(c => _mapper.Map<GetCharacterResponseDto>(c))
                .ToListAsync();
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());

                if (character is null)
                {
                    throw new Exception($"Character with Id '{id}' not found");
                }

                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Characters
                    .Where(c => c.User!.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCharacterResponseDto>(c))
                    .ToListAsync();
            }

            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
            var dbCharacters = await _context.Characters.Where(c => c.User!.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();
            var dbCharacter = await _context.Characters
                .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId() );
            serviceResponse.Data = _mapper.Map<GetCharacterResponseDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();
            try
            {

                var character = await _context.Characters
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if (character is null || character.User!.Id != GetUserId())
                {
                    throw new Exception($"Character with Id '{updatedCharacter.Id}' not found");
                }

                _mapper.Map(updatedCharacter, character);

                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strenght = updatedCharacter.Strenght;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterResponseDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;


        }



    }
}