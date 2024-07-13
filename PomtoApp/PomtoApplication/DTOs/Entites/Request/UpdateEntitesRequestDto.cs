using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using PomtoApplication.ShareDto;

namespace PomtoApplication.DTOs.Entites.Request
{
    public class UpdateEntitesRequestDto : DeleteDto
    {
        [Required(ErrorMessage = "Campo Necessário")]
        [DisplayName("Nome da Empresa")]
        public string? NomeEmpresa { get; set; }

        [Required(ErrorMessage = "Nome de Usuário necessário")]
        [DisplayName("Nome de Usuário")]
        public string? NomeUsuario { get; set; }

        [DisplayName("Detalhes da Empresa")]
        public string? DetalhesEmpresa { get; set; }

        [DisplayName("Localização da Empresa")]
        public string? LocalizacaoEmpresa { get; set; }

        [Required(ErrorMessage = "Password necessária")]
        [DataType(DataType.Password)]
        [DisplayName("Senha de Usuário")]
        public string? Password { get; set; }

        [EmailAddress]
        [DisplayName("Email da Empresa")]
        public string? EmailEmpresa { get; set; }

        [Required(ErrorMessage = "Campo Necessário")]
        [DisplayName("Número de Identificação Fiscal da Empresa")]
        public string NIF { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Necessário")]
        [DisplayName("Tipo de Empresa")]
        public string? TipoEmpresa { get; set; }

        [DisplayName("Número de Telefone da Empresa")]
        public List<string>? NumeroTelefone { get; set; } = new();

        [Required(ErrorMessage = "Campo Necessário")]
        [DisplayName("Responsável da Empresa")]
        public string? ContaResponsavel { get; set; }
    }
}