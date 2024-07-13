using Microsoft.EntityFrameworkCore;
using PomtoDomain.Interfaces;
using PomtoDomain.Model;
using PomtoInfraData.PomtoContext;

namespace PomtoInfraData.Repository
{
    public class ProfissaoUsuarioRPL : IProfissaoUsuario
    {
        private readonly PomtoDbContext _context;

        public ProfissaoUsuarioRPL(PomtoDbContext context)
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

        public async Task<Pt_ProfissaoUsuario> GetByIdAsync(int id)
        {
            return await _context.ProfissaoUsuarios.Where(w => w.ID == id && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<Pt_ProfissaoUsuario> GetByNameAsync(string name)
        {
            return await _context.ProfissaoUsuarios.Where(w => w.NomeProfissao == name && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<List<Pt_ProfissaoUsuario>> GetListAsync()
        {
            return await _context.ProfissaoUsuarios.Where(w => w.Status == true).ToListAsync();
        }

        public async Task<int> SaveAsync(Pt_ProfissaoUsuario model)
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