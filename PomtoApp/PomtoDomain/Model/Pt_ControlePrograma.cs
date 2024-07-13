using PomtoDomain.ShareData;

namespace PomtoDomain.Model
{
    public class Pt_ControlePrograma : ModelBase
    {
        public string NomePrograma { get; set; } = string.Empty;
        public int TempoAtivo { get; set; }
        public int UsuarioID { get; set; }
        public Pt_Usuario? Usuario { get; set; }
    }
}
