namespace PomtoApplication.DTOs.WorkStation.Request.CreateDto
{
    public class CreateEndWorkRequestDTO
    {
        public List<string> TarefaDescricao { get; set; } = new();
        public List<string> Imagens { get; set; } = new();
    }
}
