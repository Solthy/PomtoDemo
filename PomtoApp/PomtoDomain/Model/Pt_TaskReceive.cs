using PomtoDomain.ShareData;

namespace PomtoDomain.Model
{
    public class Pt_TaskReceive : ModelBase
    {
        public int TaskID { get; set; }
        public Pt_Task? Task { get; set; }
        public int UsuarioReceptorID { get; set; }
        public Pt_Usuario? Usuario { get; set; }
    }
}
