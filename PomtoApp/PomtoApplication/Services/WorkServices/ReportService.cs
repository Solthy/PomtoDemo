using PomtoApplication.Helpers;
using PomtoDomain.Interfaces;

namespace PomtoApplication.Services.WorkRPL
{
    public class ReportService : PathHelper
    {
        private readonly IAutenticacao _loginRPL;
        private readonly IUsuario _usuario;

        public ReportService(IAutenticacao loginRPL, IUsuario usuario)
        {
            _loginRPL=loginRPL;
            _usuario=usuario;
        }

        public async Task GenerateReport(double value)
        {
            var login = await _loginRPL.GetLastLoginAsync();
            var userModel = await _usuario.GetByIdAsync(login.UserId);
            string mensagem = $"Valor do dia {DateTime.Now:dd/MM/yyyy}: {value} Kz (Cobrado por: {userModel.TipoPagamento})\n\n";

            if (!File.Exists(WorkFile))
            {
                File.Create(WorkFile).Close();
            }

            using (StreamWriter writer = File.AppendText(WorkFile))
            {
                writer.WriteLine(mensagem);
            }
        }
    }
}
