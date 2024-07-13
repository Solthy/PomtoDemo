using Microsoft.EntityFrameworkCore;
using PomtoDomain.Interfaces;
using PomtoDomain.Model;
using PomtoInfraData.PomtoContext;

namespace PomtoInfraData.Repository
{
    public class ControleProgramaRPL : IControlePrograma
    {
        private readonly PomtoDbContext _context;

        public ControleProgramaRPL(PomtoDbContext context)
        {
            _context=context;
        }

        public async Task<int> DisableAsync(int id)
        {
            var model = await GetByIdAsync(id);

            model.DataRemocao = DateTime.Now;
            model.Status = false;

            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DisableProgramWork(int userId, int tempo)
        {
            var model = await GetByIdAsync(userId);

            model.TempoAtivo = tempo;
            model.DataRemocao = DateTime.Now;
            model.Status = false;

            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<Pt_ControlePrograma> GetByIdAsync(int id)
        {
            return await _context.ControleProgramas.Where(w => w.ID == id && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<List<Pt_ControlePrograma>> GetListAsync()
        {
            return await _context.ControleProgramas.Where(w => w.Status == true).ToListAsync();
        }

        public async Task<int> SaveAsync(Pt_ControlePrograma model)
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

        public async Task<List<Pt_ControlePrograma>> TakeListProgramByUser(int userId)
        {
            return await _context.ControleProgramas.Where(w => w.UsuarioID == userId && w.Status == true).ToListAsync();
        }

        public async Task<List<Pt_ControlePrograma>> TakeListProgramWork(bool status)
        {
            return await _context.ControleProgramas.Where(w => w.Status == status).ToListAsync();
        }
    }
}