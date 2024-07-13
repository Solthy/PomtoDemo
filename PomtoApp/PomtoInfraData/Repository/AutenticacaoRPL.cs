using Microsoft.EntityFrameworkCore;
using PomtoDomain.Interfaces;
using PomtoDomain.Model;
using PomtoInfraData.PomtoContext;

namespace PomtoInfraData.Repository
{
    public class AutenticacaoRPL : IAutenticacao
    {
        private readonly PomtoDbContext _context;

        public AutenticacaoRPL(PomtoDbContext context)
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

        public async Task<Pt_Login> GetByIdAsync(int id)
        {
            return await _context.Logins.Where(w => w.ID == id && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<List<Pt_Login>> GetListAsync()
        {
            return await _context.Logins.Where(w => w.Status == true).ToListAsync();
        }

        public async Task<int> SaveAsync(Pt_Login model)
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

        public async Task<int> SaveLogoutAsync(Pt_Login model)
        {
            var modelLogin = new Pt_Login
            {
                ID = model.ID,
                UserId = model.UserId,
                CompanyId = model.CompanyId,
                Status = false,
                DataAtualizacao = DateTime.Now
            };

            _context.Update(model);

            return await _context.SaveChangesAsync();
        }

        public async Task<Pt_Login> GetLastLoginAsync()
        {
            return await _context.Logins.OrderByDescending(o => o.ID).Where(w => w.Status == true).FirstOrDefaultAsync();
        }
    }
}