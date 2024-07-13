using PomtoApplication.ShareDto;

namespace PomtoApplication.DTOs.WorkStation.Request.UpdateDto
{
    public class UpdateProgramToWorkRequestDTO : DeleteDto
    {
        public string NomePrograma { get; set; } = string.Empty;
        public int TempoAtividade { get; set; }
    }
}