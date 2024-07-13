using Microsoft.EntityFrameworkCore;
using PomtoDomain.Interfaces;
using PomtoDomain.Model;
using PomtoInfraData.PomtoContext;

namespace PomtoInfraData.Repository
{
    public class FimTrabalhoRPL : IFimTrabalho
    {
        private readonly PomtoDbContext _context;

        public FimTrabalhoRPL(PomtoDbContext context)
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

        public async Task<Pt_FimTrabalho> GetByIdAsync(int id)
        {
            return await _context.FimTrabalhos.Where(w => w.ID == id && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<List<Pt_FimTrabalho>> GetListAsync()
        {
            return await _context.FimTrabalhos.Where(w => w.Status == true).ToListAsync();
        }

        public async Task<int> GetTotalDaysByPeriodAsync(int userId, int period)
        {
            if (period >= 0 && period <= 12)
            {
                var days = await _context.FimTrabalhos.Where(w => w.UsuarioID == userId && w.DataCriacao.Month == period).Select(s => s.ID).ToListAsync();

                return days.Count;
            }
            else
            {
                return 0;
            }
        }

        public async Task<decimal> GetTotalRemunerationByPeriodAsync(int userId, int period)
        {
            if (period >= 0 && period <= 12)
            {
                var remuneracao = await _context.FimTrabalhos.Where(w => w.UsuarioID == userId && w.DataCriacao.Month == period).Select(s => s.Remuneracao).ToListAsync();

                return remuneracao.Sum();
            }
            else
            {
                return 0;
            }
        }

        public async Task<List<Pt_FimTrabalho>> GetWorkByUserAsync(int userId)
        {
            return await _context.FimTrabalhos.Where(w => w.UsuarioID == userId && w.Status == true).ToListAsync();
        }

        public async Task<int> SaveAsync(Pt_FimTrabalho model)
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
    }
}