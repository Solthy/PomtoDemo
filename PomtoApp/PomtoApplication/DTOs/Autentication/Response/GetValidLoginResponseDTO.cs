using PomtoApplication.ShareDto;

namespace PomtoApplication.DTOs.Autentication.Response
{
    public class GetValidLoginResponseDTO : DeleteDto
    {
        public string UserName { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public string TipoLicensa { get; set; } = string.Empty;
    }
}