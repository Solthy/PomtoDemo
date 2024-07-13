using PomtoDomain.ShareData;

namespace PomtoDomain.Model
{
    public class Pt_Licenca : ModelBase
    {
        public string SerialNumber { get; set; } = string.Empty;
        public DateTime ExpirateDate { get; set; }
        public int UsuarioID { get; set; }
        public Pt_Usuario? Usuario { get; set; }
        public int EmpresaID { get; set; }
        public Pt_Empresa? Empresa { get; set; }
        public int TipoLicensaID { get; set; }
        public Pt_TipoLicensa? TipoLicensa { get; set; }
    }
}
