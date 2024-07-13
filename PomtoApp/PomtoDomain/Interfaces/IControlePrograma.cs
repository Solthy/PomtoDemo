using PomtoDomain.Model;
using PomtoDomain.ShareData;

namespace PomtoDomain.Interfaces
{
    public interface IControlePrograma : IBaseInterface<Pt_ControlePrograma>
    {
        Task<List<Pt_ControlePrograma>> TakeListProgramByUser(int userId);
        Task<int> DisableProgramWork(int userId, int tempo);
        Task<List<Pt_ControlePrograma>> TakeListProgramWork(bool status);
    }
}