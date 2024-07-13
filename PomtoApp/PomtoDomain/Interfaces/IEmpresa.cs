using PomtoDomain.Model;
using PomtoDomain.ShareData;

namespace PomtoDomain.Interfaces
{
    public interface IEmpresa : IBaseInterface<Pt_Empresa>
    {
        Task<Pt_Empresa> FindEmpresaAsync(string search);
        Task<Pt_Empresa> CredentialEmpresaAsync(string username, string password);
    }
}