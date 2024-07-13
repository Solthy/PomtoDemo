using PomtoApplication.Helpers;
using PomtoApplication.ShareDto;
using PomtoDomain.Interfaces;
using PomtoDomain.Model;
using System.Diagnostics;

namespace PomtoApplication.Services.WorkRPL
{
    public class StartProcessService : PathHelper
    {
        private readonly IControlePrograma _programRPL;
        private readonly IAutenticacao _loginRPL;

        ResponseNullDto responseNullDto = new();

        public StartProcessService(IControlePrograma programRPL, IAutenticacao loginRPL)
        {
            responseNullDto.Mensagem = "Escolha um programa e comece a contar o tempo!";
            _programRPL=programRPL;
            _loginRPL=loginRPL;
        }

        public static string[] GetProcessNamesByWindowTitle(string windowTitle)
        {
            return Process.GetProcesses()
                          .Where(p => p.MainWindowTitle == windowTitle)
                          .Select(p => p.ProcessName)
                          .ToArray();
        }

        public static Process[] GetProcessesByWindowTitle(string windowTitle)
        {
            return Process.GetProcesses()
                          .Where(p => p.MainWindowTitle == windowTitle)
                          .ToArray();
        }

        public List<string> GetActiveProcesses()
        {
            List<string> activeProcesses = new List<string>();

            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                if (process.MainWindowHandle != nint.Zero && !string.IsNullOrEmpty(process.MainWindowTitle))
                {
                    activeProcesses.Add(process.MainWindowTitle);
                }
            }

            return activeProcesses;
        }

        public async Task StartProgram(string programa)
        {
            try
            {
                var processos = GetProcessesByWindowTitle(programa);

                if (processos.Length > 0)
                {
                    await _programRPL.SaveAsync(new Pt_ControlePrograma
                    {
                        NomePrograma = programa,
                        Status = true
                    });

                    responseNullDto.Mensagem = $"Contagem da aplicação {programa} foi iniciada com sucesso! Bom Trabalho para si";
                }
                else
                {
                    responseNullDto.Mensagem = $"Parece que o {programa} foi terminado ou fechado! Por favor abra ele novamente";
                }
            }
            catch (Exception ex)
            {
                responseNullDto.Mensagem = ex.Message;
            }
        }

        public async Task WriteProgram()
        {
            var listProgram = await _programRPL.TakeListProgramWork(true);

            if (listProgram == null) return;

            foreach (var program in listProgram)
            {
                var process = GetProcessNamesByWindowTitle(program.NomePrograma);

                if (process.Length > 0)
                {
                    TimeSpan exeTime = DateTime.Now - program.DataCriacao;

                    if (exeTime.TotalHours >= 1)
                    {
                        int hoursExe = (int)exeTime.TotalHours;

                        using (StreamWriter writer = File.AppendText(WorkProgramExecuteTimeFile))
                        {
                            writer.WriteLine($"O programa *{program.NomePrograma}* esteve em execução durante: *{hoursExe}* horas no dia {DateTime.Today}");
                        }
                    }
                }
            }
        }

        public async Task StopProgram()
        {
            try
            {
                var listProgram = await _programRPL.TakeListProgramWork(true);

                if (listProgram == null) return;

                foreach (var program in listProgram)
                {
                    var process = GetProcessNamesByWindowTitle(program.NomePrograma);

                    if (process.Length > 0)
                    {
                        TimeSpan exeTime = DateTime.Now - program.DataCriacao;
                        int hoursExe = (int)exeTime.TotalHours;

                        await _programRPL.DisableProgramWork(program.ID, hoursExe);
                    }
                }
            }
            catch (Exception ex)
            {
                responseNullDto.Mensagem = ex.Message;
            }
        }
    }
}
