using PomtoApplication.ShareDto;

namespace PomtoApplication.DTOs.WorkStation.Request.UpdateDto
{
    public class UpdateProgramToWork : DeleteDto
    {
        public List<string> CaminhoPasta { get; set; } = new();
    }
}
