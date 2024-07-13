using PomtoApplication.ShareDto;

namespace PomtoApplication.DTOs.Report.Response
{
    public class GetReportResponseDto : DeleteDto
    {
        public string ReferenciaDocumento { get; set; } = string.Empty;
        public string NomeDocumento { get; set; } = string.Empty;
        public string CaminhoDocumento { get; set; } = string.Empty;
        public double TamanhoDocumento { get; set; }
        public DateTime DataEmissao { get; set; } 
    }
}
