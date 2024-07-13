using PomtoApplication.Helpers;

namespace PomtoApplication.Services.WorkRPL
{
    public class MoveFilesService : PathHelper
    {
        public void MoveFiles()
        {
            string[] nameFiles = Directory.GetFiles(WorkPath);

            if (nameFiles.Length > 0)
            {
                foreach (string nameFile in nameFiles)
                {
                    string destinationFileName = Path.GetFileName(nameFile);

                    if (File.Exists(Path.Combine(WorkPastFilesPath, nameFile)))
                    {
                        destinationFileName = Path.GetFileNameWithoutExtension(nameFile) + "_" + DateTime.Now.Date + Guid.NewGuid().ToString().Substring(0,5) + Path.GetExtension(nameFile);
                        File.Move(nameFile, Path.Combine(WorkPastFilesPath, destinationFileName));
                    }
                }
            }
        }
    }
}
