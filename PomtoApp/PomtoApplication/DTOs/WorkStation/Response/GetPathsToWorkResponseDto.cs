namespace PomtoApplication.DTOs.WorkStation.Response
{
    public class GetPathsToWorkResponseDto
    {
        public List<string> Pastas { get; set; } = new();
        public DateTime DataCriacao { get; set; }
    }
}