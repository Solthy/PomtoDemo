using PomtoDomain.Model;
using PomtoDomain.ShareData;

namespace PomtoDomain.Interfaces
{
    public interface ICaminhoPasta : IBaseInterface<Pt_CaminhoPasta>
    {
        Task<List<Pt_CaminhoPasta>> TakeListPathByUser(int userId);
    }
}