using PomtoDomain.ShareData;

namespace PomtoDomain.Model
{
    public class Pt_TelefoneUsuario : ModelBase
    {
        public string? NumeroTelefone { get; set; }
        public int UsuarioID { get; set; }
        public Pt_Usuario? Usuario { get; set; }
    }
}
