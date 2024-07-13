using PomtoApplication.ShareDto;

namespace PomtoApplication.DTOs.WorkStation.Request.UpdateDto
{
    public class UpdateEndWorkRequestDTO : DeleteDto
    {
        public List<string> TarefaDescricao { get; set; } = new();
        public List<string> Imagens { get; set; } = new();
    }
}