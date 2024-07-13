using PomtoDomain.Model;
using PomtoDomain.ShareData;

namespace PomtoDomain.Interfaces
{
    public interface IUserCompany : IBaseInterface<Pt_UserCompany>
    {
        Task<Pt_UserCompany> FindUserByIdAsync(int id);
        Task<List<Pt_UserCompany>> GetListUserAsync(int idUser);
        Task<List<Pt_UserCompany>> GetListCompanyAsync(int idCompany);
    }
}
