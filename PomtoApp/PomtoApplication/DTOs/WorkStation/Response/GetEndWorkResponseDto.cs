using PomtoApplication.ShareDto;

namespace PomtoApplication.DTOs.WorkStation.Response
{
    public class GetEndWorkResponseDto : DeleteDto
    {
        public List<string> TarefaDescricao { get; set; } = new();
        public DateTime DataConclusao { get; set; }
    }
}
