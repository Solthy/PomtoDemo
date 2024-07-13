using Microsoft.EntityFrameworkCore;
using PomtoDomain.Interfaces;
using PomtoDomain.Model;
using PomtoInfraData.PomtoContext;

namespace PomtoInfraData.Repository
{
    public class DocumentoRPL : IDocumento
    {
        private readonly PomtoDbContext _context;

        public DocumentoRPL(PomtoDbContext context)
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

        public async Task<Pt_Documento> GetByIdAsync(int id)
        {
            return await _context.Documentos.Where(w => w.ID == id && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<Pt_Documento> GetDocumentByUserAsync(int id)
        {
            return await _context.Documentos.Where(w => w.UsuarioId == id && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<List<Pt_Documento>> GetListAsync()
        {
            return await _context.Documentos.Where(w => w.Status == true).ToListAsync();
        }

        public async Task<List<Pt_Documento>> GetListDocumentByUserAsync(int id)
        {
            return await _context.Documentos.Where(w => w.UsuarioId == id && w.Status == true).ToListAsync();
        }

        public async Task<int> SaveAsync(Pt_Documento model)
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
