using PomtoDomain.Model;
using PomtoDomain.ShareData;

namespace PomtoDomain.Interfaces
{
    public interface IAutenticacao : IBaseInterface<Pt_Login>
    {
        Task<Pt_Login> GetLastLoginAsync();
        Task<int> SaveLogoutAsync(Pt_Login model);
    }
}
