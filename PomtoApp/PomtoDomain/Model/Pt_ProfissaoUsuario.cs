using PomtoDomain.ShareData;

namespace PomtoDomain.Model
{
    public class Pt_ProfissaoUsuario : ModelBase
    {
        public string? NomeProfissao { get; set; }
        public Pt_Usuario? Usuario { get; set; }
    }
}
