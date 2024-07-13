using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PomtoApplication.DTOs.Autentication.Request
{
    public class CreateLicenseRequestDto
    {
        [Required(ErrorMessage = "Número de Série Necessário")]
        [StringLength(12, ErrorMessage = "Insira 12 Caracteres", MinimumLength = 12)]
        [DisplayName("Série da Licença")]
        public string NumberLicensa { get; set; } = string.Empty;
        public string TipoLicensa { get; set; } = string.Empty;
        public DateTime DataExpiracao { get; set; }
    }
}
