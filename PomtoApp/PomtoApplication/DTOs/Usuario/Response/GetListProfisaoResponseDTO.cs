using PomtoApplication.ShareDto;

namespace PomtoApplication.DTOs.Usuario.Response
{
    public class GetListProfisaoResponseDTO : DeleteDto
    {
        public List<string> Profissao { get; set; } = new();
    }
}
