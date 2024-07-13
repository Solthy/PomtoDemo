namespace PomtoApplication.Helpers
{
    public class PathHelper 
    {
        #region Pasta Do Pomto
        public string WorkPath { get; } = $@"{Environment.SpecialFolder.MyDocuments}\Pomto\Trabalho";
        public string WorkPastFilesPath { get; } = $@"{Environment.SpecialFolder.MyDocuments}\Pomto\Registros Passados";
        #endregion

        #region Ficheiro do Pomto
        public string WorkFile { get; } = $@"{Environment.SpecialFolder.MyDocuments}\Pomto\Trabalho\Tarefas.txt";
        public string WorkProgramExecuteTimeFile { get; } = $@"{Environment.SpecialFolder.MyDocuments}\Pomto\Trabalho\Programas Executados.txt";
        public string WorkPremiumFilePDF { get; } = $@"{Environment.SpecialFolder.MyDocuments}\Pomto\Trabalho Premium\Relatório de Tarefas.pdf";
        #endregion

        #region Html File
        public string HtmlReportFilePath { get; } = @"C:/Users/Eliezer_Afonso/Documents/GitHub/PomtoApp/PomtoViewModel/Controllers/ReportRPL/WebModelReport/WebPremiumReport.html";
        #endregion
    }
}
