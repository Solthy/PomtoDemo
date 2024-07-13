using PomtoApplication.ShareDto;

namespace PomtoApplication.DTOs.Usuario.Response
{
    public class GetUserRequestDto : DeleteDto
    {
        public string NomeCompleto { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; } = DateTime.Today;
        public List<string> NumeroTelefone { get; set; } = new();
        public decimal ValorHora { get; set; }
        public string TipoPagamento { get; set; } = string.Empty;
        public string Profissao { get; set; } = string.Empty;
        public List<string> Empresa { get; set; } = new();
    }
}