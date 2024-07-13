using PomtoDomain.ShareData;

namespace PomtoDomain.Model
{
    public class Pt_CaminhoPasta : ModelBase
    {
        public string NomePath { get; set; } = string.Empty;
        public int UsuarioID { get; set; }
        public Pt_Usuario? Usuario { get; set; }
    }
}