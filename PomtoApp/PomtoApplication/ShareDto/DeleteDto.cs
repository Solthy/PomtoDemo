using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PomtoApplication.ShareDto
{
    public class DeleteDto 
    {
        [Required(ErrorMessage = "Campo Necessário")]
        [DisplayName("Número do Registro")]
        public int Id { get; set; }
    }
}
