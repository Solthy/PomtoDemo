using PomtoDomain.Model;
using PomtoDomain.ShareData;

namespace PomtoDomain.Interfaces
{
    public interface IFimTrabalho : IBaseInterface<Pt_FimTrabalho>
    {
        Task<List<Pt_FimTrabalho>> GetWorkByUserAsync(int userId);
        Task<decimal> GetTotalRemunerationByPeriodAsync(int userId, int period);
        Task<int> GetTotalDaysByPeriodAsync(int userId, int period);
    }
}