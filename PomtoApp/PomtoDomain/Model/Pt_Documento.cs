using PomtoDomain.ShareData;

namespace PomtoDomain.Model
{
    public class Pt_Documento : ModelBase
    {
        public string ReferenciaDocumento { get; set; } = string.Empty;
        public string NomeDocumento { get; set; } = string.Empty;
        public double TamanhoDocumento { get; set; }
        public string ExtensaoDocumento { get; set; } = string.Empty;
        public string CaminhoDocumento { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
        public Pt_Usuario? Usuario { get; set; }
    }
}
