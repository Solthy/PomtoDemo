using PomtoDomain.ShareData;

namespace PomtoDomain.Model
{
    public class Pt_TelefoneEmpresa : ModelBase
    {
        public string? NumeroTelefone { get; set; }
        public int EmpresaID { get; set; }
        public Pt_Empresa? Empresa { get; set; }
    }
}