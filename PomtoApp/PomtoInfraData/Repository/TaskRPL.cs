using Microsoft.EntityFrameworkCore;
using PomtoDomain.Interfaces;
using PomtoDomain.Model;
using PomtoInfraData.PomtoContext;

namespace PomtoInfraData.Repository
{
    public class TaskRPL : ITask
    {
        private readonly PomtoDbContext _context;

        public TaskRPL(PomtoDbContext context)
        {
            _context=context;
        }

        #region Task

        public async Task<int> DisableAsync(int id)
        {
            var model = await GetByIdAsync(id);

            model.DataRemocao = DateTime.Now;
            model.Status = false;

            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<Pt_Task> GetByIdAsync(int id)
        {
            return await _context.Tasks.Where(w => w.ID == id && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<int> SaveAsync(Pt_Task model)
        {
            if (model.ID != 0)
            {
                await _context.AddAsync(model);
            }
            else
            {
                model.DataAtualizacao = DateTime.Now;

                _context.Update(model);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<List<Pt_Task>> FindTaskForUserAsync(int userId, string urgencia, string nomeTarefa)
        {
            return await _context.Tasks.Where(w => w.UsuarioRemetenteID == userId || w.NomeTarefa == nomeTarefa || w.UrgenciaTarefa == urgencia).ToListAsync();
        }

        public async Task<List<Pt_Task>> GetListAsync()
        {
            return await _context.Tasks.Where(w => w.Status == true).ToListAsync();
        }

        #endregion

        #region Task Receive

        public async Task<int> DisableReceiveTaskAsync(int userId)
        {
            var model = await GetByIdTaskReceiveForUserAsync(userId);

            model.DataRemocao = DateTime.Now;
            model.Status = false;

            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Pt_Task>> FindTaskReceiveForUserAsync(int userId, string urgencia, string nomeTarefa)
        {
            int taskId = await _context.TaskReceives.Where(w => w.UsuarioReceptorID == userId && w.Status == true).Select(s => s.TaskID).FirstOrDefaultAsync();

            return await _context.Tasks.Where(w => w.ID == taskId || w.NomeTarefa == nomeTarefa || w.UrgenciaTarefa == urgencia).ToListAsync();
        }

        public async Task<List<Pt_Task>> GetAllTaskReceiveForUserAsync(int userId)
        {
            int taskId = await _context.TaskReceives.Where(w => w.UsuarioReceptorID == userId && w.Status == true).Select(s => s.TaskID).FirstOrDefaultAsync();

            return await _context.Tasks.Where(w => w.Status == true && w.ID == taskId).ToListAsync();
        }

        public async Task<Pt_TaskReceive> GetByIdTaskReceiveForUserAsync(int userId)
        {
            return await _context.TaskReceives.Where(w => w.ID == userId && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<int> SaveReceiveTaskAsync(Pt_TaskReceive model)
        {
            if (model.ID != 0)
            {
                await _context.AddAsync(model);
            }
            else
            {
                model.DataAtualizacao = DateTime.Now;

                _context.Update(model);
            }

            return await _context.SaveChangesAsync();
        }

        #endregion
    }
}