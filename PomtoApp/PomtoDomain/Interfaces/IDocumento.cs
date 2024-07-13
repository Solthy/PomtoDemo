using PomtoDomain.Model;
using PomtoDomain.ShareData;

namespace PomtoDomain.Interfaces
{
    public interface IDocumento : IBaseInterface<Pt_Documento>
    {
        Task<Pt_Documento> GetDocumentByUserAsync(int id);
        Task<List<Pt_Documento>> GetListDocumentByUserAsync(int id);
    }
}
