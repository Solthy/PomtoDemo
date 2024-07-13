using Microsoft.EntityFrameworkCore;
using PomtoDomain.Interfaces;
using PomtoDomain.Model;
using PomtoInfraData.PomtoContext;

namespace PomtoInfraData.Repository
{
    public class EmpresaRPL : IEmpresa
    {
        private readonly PomtoDbContext _context;

        public EmpresaRPL(PomtoDbContext context)
        {
            _context=context;
        }

        public async Task<Pt_Empresa> CredentialEmpresaAsync(string username, string password)
        {
            return await _context.Empresas.Where(w => w.UserName == username ||
                    w.Password == password  && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<int> DisableAsync(int id)
        {
            var model = await GetByIdAsync(id);

            model.DataRemocao = DateTime.Now;
            model.Status = false;

            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<Pt_Empresa> FindEmpresaAsync(string search)
        {
            return await _context.Empresas.Where(w => w.NomeEmpresa == search || 
            w.NIF == search ||
            w.Email == search ||
            w.Morada == search && 
            w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<Pt_Empresa> GetByIdAsync(int id)
        {
            return await _context.Empresas.Where(w => w.ID == id && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<List<Pt_Empresa>> GetListAsync()
        {
            return await _context.Empresas.Where(w => w.Status == true).ToListAsync();
        }

        public async Task<int> SaveAsync(Pt_Empresa model)
        {
            if (model.ID == 0)
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