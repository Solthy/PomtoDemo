using Microsoft.EntityFrameworkCore;
using PomtoDomain.Interfaces;
using PomtoDomain.Model;
using PomtoInfraData.PomtoContext;

namespace PomtoInfraData.Repository
{
    public class TelefoneEmpresaRPL : ITelefoneEmpresa
    {
        private readonly PomtoDbContext _context;

        public TelefoneEmpresaRPL(PomtoDbContext context)
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

        public async Task<Pt_TelefoneEmpresa> GetByIdAsync(int id)
        {
            return await _context.TelefoneEmpresas.Where(w => w.ID == id && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<List<Pt_TelefoneEmpresa>> GetListAsync()
        {
            return await _context.TelefoneEmpresas.Where(w => w.Status == true).ToListAsync();
        }

        public async Task<List<Pt_TelefoneEmpresa>> GetListPhonesByCompanyIdAsync(int companyId)
        {
            return await _context.TelefoneEmpresas.Where(w => w.EmpresaID == companyId && w.Status == true).ToListAsync();
        }

        public async Task<int> SaveAsync(Pt_TelefoneEmpresa model)
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

        public async Task<int> SaveRangeAsync(List<Pt_TelefoneEmpresa> model)
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