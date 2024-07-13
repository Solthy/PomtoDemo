using PomtoDomain.Model;
using PomtoDomain.ShareData;

namespace PomtoDomain.Interfaces
{
    public interface ITipoLicenca : IBaseInterface<Pt_TipoLicensa>
    {
        Task<Pt_TipoLicensa> FindTypeLicenseAsync(string search);
    }
}