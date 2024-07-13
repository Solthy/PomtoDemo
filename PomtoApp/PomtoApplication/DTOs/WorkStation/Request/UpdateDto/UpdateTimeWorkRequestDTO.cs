using PomtoApplication.ShareDto;

namespace PomtoApplication.DTOs.WorkStation.Request.UpdateDto
{
    public class UpdateTimeWorkRequestDTO : DeleteDto
    {
        public int HoraTrabalho { get; set; }
    }
}