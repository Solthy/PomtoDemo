using PomtoDomain.ShareData;

namespace PomtoDomain.Model
{
    public class Pt_Task : ModelBase
    {
        public string? NomeTarefa { get; set; }
        public string? DescricaoTarefa { get; set; }
        public string? UrgenciaTarefa { get; set; }
        public int UsuarioRemetenteID { get; set; }
        public Pt_Usuario? Usuario { get; set; }
        public ICollection<Pt_TaskReceive>? TaskReceives { get; set; }
    }
}
