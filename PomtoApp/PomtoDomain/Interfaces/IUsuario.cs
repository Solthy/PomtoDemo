using PomtoDomain.Model;
using PomtoDomain.ShareData;

namespace PomtoDomain.Interfaces
{
    public interface IUsuario : IBaseInterface<Pt_Usuario>
    {
        Task<Pt_Usuario> FindUserAsync(string search);
        Task<Pt_Usuario> CredentialUserAsync(string username, string password);
    }
}