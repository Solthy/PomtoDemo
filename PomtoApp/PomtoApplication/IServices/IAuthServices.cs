using PomtoApplication.DTOs.Autentication.Request;
using PomtoApplication.DTOs.Autentication.Response;
using PomtoApplication.ShareDto;

namespace PomtoApplication.IServices
{
    public interface IAuthServices
    {
        Task<ResponseNullDto> ValidateLoginAsync(LoginRequestDto model);
        Task<ResponseNullDto> LogoutAsync(LoginRequestDto model);
        Task<ResponseDto<GetLicenseResponseDto>> TakeDataLicenseAsync(string user);
        Task<ResponseNullDto> SaveItemAsync(CreateLicenseRequestDto model);
        Task<ResponseNullDto> UpdateItemAsync(UpdateLicenseRequestDto model);
        Task<ResponseNullDto> RecoveryCredentialsAsync(RecoveryLoginRequestDto model);
    }
}
