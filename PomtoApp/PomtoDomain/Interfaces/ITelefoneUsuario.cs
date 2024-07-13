using PomtoDomain.Model;
using PomtoDomain.ShareData;

namespace PomtoDomain.Interfaces
{
    public interface ITelefoneUsuario : IBaseInterface<Pt_TelefoneUsuario>
    {
        Task<Pt_TelefoneUsuario> GetPhoneByUserIdAsync(int userId);
        Task<List<Pt_TelefoneUsuario>> GetListPhonesByUsuarioIdAsync(int userId);
        Task<int> SaveRangeAsync(List<Pt_TelefoneUsuario> model);
    }
}