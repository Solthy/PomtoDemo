using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using PomtoApplication.ShareDto;

namespace PomtoApplication.DTOs.Usuario.Request
{
    public class UpdateUserRequestDto : DeleteDto
    {
        [Required(ErrorMessage = "Campo Necessário")]
        [DisplayName("Nome Completo")]
        [StringLength(60, ErrorMessage = "Min 10 e Máx 60 Caracteres", MinimumLength = 10)]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Necessário")]
        [DisplayName("Nome Completo")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Necessário")]
        [StringLength(30, ErrorMessage = "Precisa inserir no mínimo 8 dígitos", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Necessário")]
        [StringLength(30, ErrorMessage = "Precisa inserir no mínimo 8 dígitos", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Necessário")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email do Usuário")]
        public string UserEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Necessário")]
        [DataType(DataType.DateTime)]
        [DisplayName("Data de Nascimento")]
        public DateTime? DataNascimento { get; set; } = DateTime.Today;

        [StringLength(9, ErrorMessage = "Precisa inserir no mínimo 9 dígitos", MinimumLength = 9)]
        [DisplayName("Número de Telefone")]
        public List<string> NumeroTelefone { get; set; } = new();

        [Required(ErrorMessage = "Campo Necessário")]
        [DisplayName("Valor de Remuneração")]
        public decimal ValorHora { get; set; }

        [Required(ErrorMessage = "Campo Necessário")]
        [DisplayName("Método de Remuneração")]
        public string TipoPagamento { get; set; } = string.Empty;

        [DisplayName("Morada")]
        public string Morada { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Necessário")]
        [DisplayName("Profissão")]
        public string Profissao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Necessário")]
        [DisplayName("Nome da Empresa")]
        public string Empresa { get; set; } = string.Empty;
    }
}