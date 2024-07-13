using PomtoDomain.Model;
using PomtoDomain.ShareData;

namespace PomtoDomain.Interfaces
{
    public interface ILicenca : IBaseInterface<Pt_Licenca>
    {
        string GenerateLicenseAsync();
        Task<Pt_Licenca> FindLicenseAsync(string search);
        Task<Pt_Licenca> FindListLicenseByUserAsync(int id, string typeUser);
    }
}