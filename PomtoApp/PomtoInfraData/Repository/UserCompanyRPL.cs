using Microsoft.EntityFrameworkCore;
using PomtoDomain.Interfaces;
using PomtoDomain.Model;
using PomtoInfraData.PomtoContext;

namespace PomtoInfraData.Repository
{
    public class UserCompanyRPL : IUserCompany
    {
        private readonly PomtoDbContext _context;

        public UserCompanyRPL(PomtoDbContext context)
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

        public async Task<Pt_UserCompany> FindUserByIdAsync(int id)
        {
            return await _context.UserCompanies.Where(w => w.ID == id || w.UserId == id || w.CompanyId == id && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<Pt_UserCompany> GetByIdAsync(int id)
        {
            return await _context.UserCompanies.Where(w => w.ID == id && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<List<Pt_UserCompany>> GetListAsync()
        {
            return await _context.UserCompanies.Where(w => w.Status == true).ToListAsync();
        }

        public async Task<List<Pt_UserCompany>> GetListCompanyAsync(int idCompany)
        {
            return await _context.UserCompanies.Where(w => w.CompanyId == idCompany && w.Status == true).ToListAsync();
        }

        public async Task<List<Pt_UserCompany>> GetListUserAsync(int idUser)
        {
            return await _context.UserCompanies.Where(w => w.UserId == idUser && w.Status == true).ToListAsync();
        }

        public async Task<int> SaveAsync(Pt_UserCompany model)
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
