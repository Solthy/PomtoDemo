using PomtoApplication.ShareDto;

namespace PomtoApplication.DTOs.Entites.Response
{
    public class GetPerfilEntitesResponseDto : DeleteDto
    {
        public string? NomeEmpresa { get; set; }
        public string? EmailEmpresa { get; set; }
        public string NIF { get; set; } = string.Empty;
        public string? TipoEmpresa { get; set; }
        public string? ContaResponsavel { get; set; }
        public List<string>? NumeroTelefone { get; set; } = new();
        public List<GetEntitesResponseDto> ListaUsuarios { get; set; } = new();
    }
}