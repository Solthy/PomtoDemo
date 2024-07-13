namespace PomtoDomain.Model
{
    public class Pt_Usuario : Pt_Autenticacao
    {
        public string? NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal Pagamento { get; set; }
        public string TipoPagamento { get; set; } = string.Empty;
        public bool ResponsavelAtribuido { get; set; }
        public string? Morada { get; set; }
        public int ProfissaoID { get; set; }
        public Pt_ProfissaoUsuario? ProfissaoUsuario { get; set; }
        public ICollection<Pt_CaminhoPasta>? CaminhoPastas { get; set; }
        public ICollection<Pt_ControlePrograma>? ControleProgramas { get; set; }
        public ICollection<Pt_Documento>? Documentos { get; set; }
        public ICollection<Pt_FimTrabalho>? FimTrabalhos { get; set; }
        public ICollection<Pt_Licenca>? Licencas { get; set; }
        public ICollection<Pt_Login>? Logins { get; set; }
        public ICollection<Pt_Task>? Tasks { get; set; }
        public ICollection<Pt_TaskReceive>? TaskReceives { get; set; }
        public ICollection<Pt_TelefoneUsuario>? TelefoneUsuarios { get; set; }
        public ICollection<Pt_UserCompany>? Empresas { get; set; }
    }
}
