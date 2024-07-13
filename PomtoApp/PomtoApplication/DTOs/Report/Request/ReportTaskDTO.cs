namespace PomtoApplication.DTOs.Report.Request
{
    public class ReportTaskDTO
    {
        public string? NomeFicheiro { get; set; }
        public string? NomePasta { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }

    public class ReportProgamDTO
    {
        public string? NomePrograma { get; set; }
        public DateTime? DataAlteracaoPrograma { get; set; }
    }
}
