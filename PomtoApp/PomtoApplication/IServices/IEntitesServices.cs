using PomtoApplication.DTOs.Entites.Request;
using PomtoApplication.DTOs.Entites.Response;
using PomtoApplication.ShareDto;

namespace PomtoApplication.IServices
{
    public interface IEntitesServices
    {
        Task<ResponseNullDto> SaveItemAsync(CreateEntitesRequestDto model);
        Task<ResponseNullDto> UpdateItemAsync(UpdateEntitesRequestDto model);
        Task<ResponseDto<GetEntitesResponseDto>> GetItemAsync();
        Task<ResponseDto<GetPerfilEntitesResponseDto>> GetPerfilItemAsync();
    }
}
