using Microsoft.EntityFrameworkCore;
using PomtoDomain.Interfaces;
using PomtoDomain.Model;
using PomtoInfraData.PomtoContext;

namespace PomtoInfraData.Repository
{
    public class CaminhoPastaRPL : ICaminhoPasta
    {
        private readonly PomtoDbContext _context;

        public CaminhoPastaRPL(PomtoDbContext context)
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

        public async Task<Pt_CaminhoPasta> GetByIdAsync(int id)
        {
            return await _context.CaminhoPastas.Where(w => w.ID == id && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<List<Pt_CaminhoPasta>> GetListAsync()
        {
            return await _context.CaminhoPastas.Where(w => w.Status == true).ToListAsync();
        }

        public async Task<int> SaveAsync(Pt_CaminhoPasta model)
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

        public async Task<List<Pt_CaminhoPasta>> TakeListPathByUser(int userId)
        {
            return await _context.CaminhoPastas.Where(w => w.UsuarioID == userId && w.Status == true).ToListAsync();
        }
    }
}