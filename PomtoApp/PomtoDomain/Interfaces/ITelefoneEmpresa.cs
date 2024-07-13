using PomtoDomain.Model;
using PomtoDomain.ShareData;

namespace PomtoDomain.Interfaces
{
    public interface ITelefoneEmpresa : IBaseInterface<Pt_TelefoneEmpresa>
    {
        Task<List<Pt_TelefoneEmpresa>> GetListPhonesByCompanyIdAsync(int companyId);
        Task<int> SaveRangeAsync(List<Pt_TelefoneEmpresa> model);
    }
}