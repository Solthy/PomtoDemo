namespace PomtoApplication.DTOs.WorkStation.Request.CreateDto
{
    public class CreateProgramToWorkRequestDTO
    {
        public string NomePrograma { get; set; } = string.Empty;
        public int TempoAtividade { get; set; }
    }
}