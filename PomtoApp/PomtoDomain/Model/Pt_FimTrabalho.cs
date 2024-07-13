using PomtoDomain.ShareData;

namespace PomtoDomain.Model
{
    public class Pt_FimTrabalho : ModelBase
    {
        public string? DescricaoAtividade { get; set; }
        public decimal Remuneracao { get; set; }
        public int HoraTrabalho { get; set; }
        public int UsuarioID { get; set; }
        public Pt_Usuario? Usuario { get; set; }
        public int TaskID { get; set; }
        public Pt_Task? Task { get; set; }
    }
}
