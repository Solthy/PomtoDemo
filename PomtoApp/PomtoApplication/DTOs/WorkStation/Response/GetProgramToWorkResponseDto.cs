using PomtoApplication.ShareDto;

namespace PomtoApplication.DTOs.WorkStation.Response
{
    public class GetProgramToWorkResponseDto : DeleteDto
    {
        public string NomePrograma { get; set; } = string.Empty;
        public int TempoAtividade { get; set; }
    }
}
