using PomtoApplication.ShareDto;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PomtoApplication.DTOs.Autentication.Request
{
    public class LoginRequestDto : DeleteDto
    {
        [Required(ErrorMessage = "Nome de Usuário necessário")]
        [DisplayName("Nome de Usuário")]
        public string? NomeUsuario { get; set; }

        [Required(ErrorMessage = "Password necessária")]
        [DataType(DataType.Password)]
        [DisplayName("Senha de Usuário")]
        public string? Password { get; set; }
    }
}
