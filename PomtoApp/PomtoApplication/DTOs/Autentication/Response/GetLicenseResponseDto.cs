using PomtoApplication.ShareDto;
using System.ComponentModel;

namespace PomtoApplication.DTOs.Autentication.Response
{
    public class GetLicenseResponseDto : DeleteDto
    {
        [DisplayName("Série da Licença")]
        public string NumberLicensa { get; set; } = string.Empty;
        public string TipoLicensa { get; set; } = string.Empty;
        public DateTime DataExpiracao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}