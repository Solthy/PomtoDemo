using PomtoApplication.Helpers;
using PomtoDomain.Interfaces;

namespace PomtoApplication.Services.WorkRPL
{
    public class ReadPathService : PathHelper
    {
        private readonly ICaminhoPasta _caminhoPasta;

        public ReadPathService(ICaminhoPasta caminhoPasta)
        {
            _caminhoPasta=caminhoPasta;
            if (!Directory.Exists(WorkPath))
            {
                Directory.CreateDirectory(WorkPath);
            }
            if (!Directory.Exists(WorkPastFilesPath))
            {
                Directory.CreateDirectory(WorkPastFilesPath);
            }
        }

        public FileInfo LastDirectoryFileChange(string pasta)
        {
            var directory = new DirectoryInfo(pasta);
            var arquivos = directory.GetFiles("*.*", SearchOption.AllDirectories)
                                    .Where(f => f.Extension != ".vsidx")
                                    .OrderByDescending(f => f.LastWriteTime)
                                    .ToArray();
            return arquivos.FirstOrDefault();
        }

        public void WriteWorkInFile(string descricao)
        {
            if (!File.Exists(WorkFile))
            {
                File.Create(WorkFile).Close();
            }

            using (StreamWriter writer = File.AppendText(WorkFile))
            {
                string mensagem = $"Data: {DateTime.Now:dd/MM/yyyy HH:mm}\nAtividade: {descricao}\n";
                writer.WriteLine(mensagem);
            }
        }

        public void ReadLastFile(List<string> pastas)
        {
            foreach (string pasta in pastas)
            {
                var ultimoArquivo = LastDirectoryFileChange(pasta);
                if (ultimoArquivo == null) continue;

                if (!File.Exists(WorkFile))
                {
                    File.Create(WorkFile).Close();
                }

                DateTime lastTimeModified = ultimoArquivo.LastWriteTime;

                string mensagem = $"Arquivo: {ultimoArquivo.Name}\nCaminho: {pasta}\nÚltima modificação: {lastTimeModified:dd/MM/yyyy HH:mm}\n";

                if (!File.ReadAllLines(WorkFile).Contains(mensagem))
                {
                    using (StreamWriter writer = File.AppendText(WorkFile))
                    {
                        writer.WriteLine(mensagem);
                    }
                }
            }
        }

        public void ReadPath(List<string> pastas)
        {
            if (pastas != null && pastas.Count != 0)
            {
                ReadLastFile(pastas);
            }
        }
    }
}