using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PomtoApplication.DTOs.Autentication.Request
{
    public class RecoveryLoginRequestDto
    {
        [Required(ErrorMessage = "Campo necessário")]
        [DisplayName("Email de Usuário")]
        [EmailAddress]
        public string EmailUser { get; set; } = string.Empty;
    }
}