
namespace test_project.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterResponseDto>> AddWeapon(AddWeaponRequestDto newWeapon);
    }
}