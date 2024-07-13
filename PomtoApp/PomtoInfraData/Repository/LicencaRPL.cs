using Microsoft.EntityFrameworkCore;
using PomtoDomain.Interfaces;
using PomtoDomain.Model;
using PomtoInfraData.PomtoContext;

namespace PomtoInfraData.Repository
{
    public class LicencaRPL : ILicenca
    {
        private readonly PomtoDbContext _context;
        private static Random random = new Random();

        public LicencaRPL(PomtoDbContext context)
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

        public async Task<Pt_Licenca> FindLicenseAsync(string search)
        {
            return await _context.Licencas.Where(w => w.SerialNumber == search || w.ExpirateDate == Convert.ToDateTime(search)).FirstOrDefaultAsync();
        }

        public string GenerateLicenseAsync()
        {
            const int licenseLength = 12;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] licenseChars = new char[licenseLength];

            for (int i = 0; i < licenseLength; i++)
            {
                licenseChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(licenseChars);
        }

        public async Task<Pt_Licenca> GetByIdAsync(int id)
        {
            return await _context.Licencas.Where(w => w.ID == id && w.Status == true).FirstOrDefaultAsync();
        }

        public async Task<List<Pt_Licenca>> GetListAsync()
        {
            return await _context.Licencas.Where(w => w.Status == true).ToListAsync();
        }

        public async Task<int> SaveAsync(Pt_Licenca model)
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

        public async Task<Pt_Licenca> FindListLicenseByUserAsync(int id, string typeUser)
        {
            if (typeUser == "Usuário")
                return await _context.Licencas.Where(w => w.Status == true && w.UsuarioID == id).FirstOrDefaultAsync();
            else
                return await _context.Licencas.Where(w => w.Status == true && w.EmpresaID == id).FirstOrDefaultAsync();
        }
    }
}