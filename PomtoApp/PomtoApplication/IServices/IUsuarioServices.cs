using PomtoApplication.DTOs.Entites.Request;
using PomtoApplication.DTOs.Report.Response;
using PomtoApplication.DTOs.Usuario.Request;
using PomtoApplication.DTOs.Usuario.Response;
using PomtoApplication.ShareDto;

namespace PomtoApplication.IServices
{
    public interface IUsuarioServices
    {
        Task<ResponseNullDto> SaveItemAsync(CreateUserRequestDto model);
        Task<ResponseNullDto> UpdateItemAsync(UpdateUserRequestDto model);
        Task<ResponseDto<GetUserRequestDto>> GetItemAsync();
        Task<ResponseDto<GetPerfilUserRequestDto>> GetPerfilItemAsync();
        Task<ResponseListDto<GetListProfisaoResponseDTO>> GetProfissaoItemAsync();
        Task<ResponseListDto<GetReportResponseDto>> GetDocumentosItemAsync();
    }
}
