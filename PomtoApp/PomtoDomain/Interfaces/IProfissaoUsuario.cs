using PomtoDomain.Model;
using PomtoDomain.ShareData;

namespace PomtoDomain.Interfaces
{
    public interface IProfissaoUsuario : IBaseInterface<Pt_ProfissaoUsuario>
    {
        Task<Pt_ProfissaoUsuario> GetByNameAsync(string name);
    }
}