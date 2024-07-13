using PomtoDomain.ShareData;

namespace PomtoDomain.Model
{
    public class Pt_TipoLicensa : ModelBase
    {
        public string NomeTipoLicensa { get; set; } = string.Empty;
        public string BeneficiosLicensa { get; set; } = string.Empty;
        public string Publico { get; set; } = string.Empty;
        public string NumeroUsuarios { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public ICollection<Pt_Licenca>? Licencas { get; set; }
    }
}
