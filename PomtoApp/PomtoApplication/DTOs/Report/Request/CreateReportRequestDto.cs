using DotLiquid;

namespace PomtoApplication.DTOs.Report.Request
{
    public class CreateReportRequestDto : ILiquidizable
    {
        public string? NomeEmpresa { get; set; }
        public double? Remuneracao { get; set; }
        public string? NomeUsuario { get; set; }
        public string? EmailUsuario { get; set; }
        public int? HorasTrabalho { get; set; }
        public DateOnly DataEmissao { get; set; } = new DateOnly();
        public List<string> Atividades { get; set; } = new();
        public List<string> Imagens { get; set; } = new();
        public List<ReportTaskDTO> DadosTrabalho { get; set; } = new();
        public List<ReportProgamDTO> DadosPrograma { get; set; } = new();

        public List<string> SaveDataToListAtivity(string ativity)
        {
            Atividades.Add(ativity);
            return Atividades;
        }

        public object ToLiquid()
        {
            var hash = new Hash();

            if (Atividades != null)
                hash["Atividades"] = Atividades;

            if (DadosTrabalho != null)
                hash["DadosTrabalho"] = DadosTrabalho;

            if (DadosPrograma != null)
                hash["DadosPrograma"] = DadosPrograma;

            if (Imagens != null)
                hash["Imagens"] = Imagens;

            hash["NomeEmpresa"] = NomeEmpresa;
            hash["Remuneracao"] = Remuneracao;
            hash["NomeUsuario"] = NomeUsuario;
            hash["EmailUsuario"] = EmailUsuario;
            hash["HorasTrabalho"] = HorasTrabalho;
            hash["DataEmissao"] = DataEmissao.ToString("dd/MM/yyyy");

            return hash;
        }
    }
}
