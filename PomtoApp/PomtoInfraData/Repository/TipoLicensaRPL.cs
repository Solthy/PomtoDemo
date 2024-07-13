using Microsoft.EntityFrameworkCore;
using PomtoDomain.Interfaces;
using PomtoDomain.Model;
using PomtoInfraData.PomtoContext;

namespace PomtoInfraData.Repository
{
    public class TipoLicensaRPL : ITipoLicenca
    {
        private readonly PomtoDbContext _context;

        public TipoLicensaRPL(PomtoDbContext context)
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

        public async Task<Pt_TipoLicensa> FindTypeLicenseAsync(string search)
        {
            return await _context.TipoLicensas.Where(w => w.NomeTipoLicensa == search || w.Publico == search || w.Preco == Convert.ToDecimal(search) && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<Pt_TipoLicensa> GetByIdAsync(int id)
        {
            return await _context.TipoLicensas.Where(w => w.ID == id && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<List<Pt_TipoLicensa>> GetListAsync()
        {
            return await _context.TipoLicensas.Where(w => w.Status == true).ToListAsync();
        }

        public async Task<int> SaveAsync(Pt_TipoLicensa model)
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