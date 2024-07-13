using Microsoft.EntityFrameworkCore;
using PomtoDomain.Interfaces;
using PomtoDomain.Model;
using PomtoInfraData.PomtoContext;

namespace PomtoInfraData.Repository
{
    public class TelefoneUsuarioRPL : ITelefoneUsuario
    {
        private readonly PomtoDbContext _context;

        public TelefoneUsuarioRPL(PomtoDbContext context)
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

        public async Task<Pt_TelefoneUsuario> GetByIdAsync(int id)
        {
            return await _context.TelefoneUsuarios.Where(w => w.ID == id && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<List<Pt_TelefoneUsuario>> GetListAsync()
        {
            return await _context.TelefoneUsuarios.Where(w => w.Status == true).ToListAsync();
        }

        public async Task<List<Pt_TelefoneUsuario>> GetListPhonesByUsuarioIdAsync(int userId)
        {
            return await _context.TelefoneUsuarios.Where(w => w.UsuarioID == userId && w.Status == true).ToListAsync();
        }

        public async Task<Pt_TelefoneUsuario> GetPhoneByUserIdAsync(int userId)
        {
            return await _context.TelefoneUsuarios.Where(w => w.UsuarioID == userId && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<int> SaveAsync(Pt_TelefoneUsuario model)
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

        public async Task<int> SaveRangeAsync(List<Pt_TelefoneUsuario> model)
        {
            foreach (var itens in model)
            {
                if (itens.ID != 0)
                {
                    await _context.AddRangeAsync(model);
                }
                else
                {
                    itens.DataAtualizacao = DateTime.Now;

                    _context.UpdateRange(model);
                }
            }

            return await _context.SaveChangesAsync();
        }
    }
}