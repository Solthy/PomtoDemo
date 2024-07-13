using PomtoDomain.ShareData;

namespace PomtoDomain.Model
{
    public class Pt_Login : ModelBase
    {
        public int UserId { get; set; }
        public Pt_Usuario? Usuario { get; set; }
        public int CompanyId { get; set; }
        public Pt_Empresa? Empresa { get; set; }
    }
}
