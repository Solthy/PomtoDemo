using PomtoApplication.DTOs.Entites.Response;
using PomtoApplication.ShareDto;

namespace PomtoApplication.DTOs.Usuario.Response
{
    public class GetPerfilUserRequestDto : DeleteDto
    {
        public string NomeCompleto { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; } = DateTime.Today;
        public decimal Pagamento { get; set; }
        public int HoraTrabalho { get; set; }
        public int DiasTrabalho { get; set; }
        public List<GetEntitesResponseDto> Empresa { get; set; } = new();
        public List<string> PastasDeTrabalho { get; set; } = new();
    }
}