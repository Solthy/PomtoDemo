using PomtoDomain.Model;
using PomtoDomain.ShareData;

namespace PomtoDomain.Interfaces
{
    public interface ITask : IBaseInterface<Pt_Task>
    {
        Task<List<Pt_Task>> GetAllTaskReceiveForUserAsync(int userId);
        Task<Pt_TaskReceive> GetByIdTaskReceiveForUserAsync(int userId);
        Task<List<Pt_Task>> FindTaskForUserAsync(int userId, string urgencia, string nomeTarefa);
        Task<List<Pt_Task>> FindTaskReceiveForUserAsync(int userId, string urgencia, string nomeTarefa);
        Task<int> SaveReceiveTaskAsync(Pt_TaskReceive model);
        Task<int> DisableReceiveTaskAsync(int userId);
    }
}