namespace PomtoDomain.Model
{
    public class Pt_Empresa : Pt_Autenticacao
    {
        public string? NomeEmpresa { get; set; }
        public string? DescricaoEmpresa { get; set; }
        public string? NIF { get; set; }
        public string? TipoEmpresa { get; set; }
        public string? Morada { get; set; }
        public string? ContaResponsavel { get; set; }
        public ICollection<Pt_Licenca>? Licencas { get; set; }
        public ICollection<Pt_Login>? Logins { get; set; }
        public ICollection<Pt_TelefoneEmpresa>? TelefoneEmpresas { get; set; }
        public ICollection<Pt_UserCompany>? Usuarios { get; set; }
    }
}
