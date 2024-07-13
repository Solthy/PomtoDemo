using Microsoft.EntityFrameworkCore;
using PomtoDomain.Interfaces;
using PomtoDomain.Model;
using PomtoInfraData.PomtoContext;

namespace PomtoInfraData.Repository
{
    public class UsuarioRPL : IUsuario
    {
        private readonly PomtoDbContext _context;

        public UsuarioRPL(PomtoDbContext context)
        {
            _context=context;
        }

        public async Task<Pt_Usuario> CredentialUserAsync(string username, string password)
        {
            return await _context.Usuarios.Where(w => w.UserName == username ||
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

        public async Task<Pt_Usuario> FindUserAsync(string search)
        {
            return await _context.Usuarios.Where(w => w.UserName == search || w.Password == search || w.NomeCompleto == search || 
            w.TipoPagamento == search ||
            w.Email == search ||
            w.Morada == search && 
            w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<Pt_Usuario> GetByIdAsync(int id)
        {
            return await _context.Usuarios.Where(w => w.ID == id && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<List<Pt_Usuario>> GetListAsync()
        {
            return await _context.Usuarios.Where(w => w.Status == true).ToListAsync();
        }

        public async Task<int> SaveAsync(Pt_Usuario model)
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