using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using PomtoDomain.Model;
using PomtoDomain.ShareData;
using PomtoInfraData.ConfiguationsDb;
using PomtoInfraData.Helpers;

namespace PomtoInfraData.PomtoContext
{
    public class PomtoDbContext : DbContext
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;

        public PomtoDbContext(DbContextOptions<PomtoDbContext> options, IDataProtectionProvider dataProtectionProvider) : base(options)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        public DbSet<Pt_CaminhoPasta> CaminhoPastas { get; set; }
        public DbSet<Pt_ControlePrograma> ControleProgramas { get; set; }
        public DbSet<Pt_Documento> Documentos { get; set; }
        public DbSet<Pt_Empresa> Empresas { get; set; }
        public DbSet<Pt_FimTrabalho> FimTrabalhos { get; set; }
        public DbSet<Pt_Licenca> Licencas { get; set; }
        public DbSet<Pt_Login> Logins { get; set; }
        public DbSet<Pt_ProfissaoUsuario> ProfissaoUsuarios { get; set; }
        public DbSet<Pt_Task> Tasks { get; set; }
        public DbSet<Pt_TaskReceive> TaskReceives { get; set; }
        public DbSet<Pt_TelefoneEmpresa> TelefoneEmpresas { get; set; }
        public DbSet<Pt_TelefoneUsuario> TelefoneUsuarios { get; set; }
        public DbSet<Pt_TipoLicensa> TipoLicensas { get; set; }
        public DbSet<Pt_UserCompany> UserCompanies { get; set; }
        public DbSet<Pt_Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var protector = _dataProtectionProvider.CreateProtector("SensiveData");

            #region EncryptedDataString
            builder
                .Entity<Pt_Autenticacao>()
                .Property(e => e.Password)
                .HasConversion(new EncryptedConverter(protector));

            builder
                .Entity<Pt_CaminhoPasta>()
                .Property(e => e.NomePath)
                .HasConversion(new EncryptedConverter(protector));

            builder
                .Entity<Pt_ControlePrograma>()
                .Property(e => e.NomePrograma)
                .HasConversion(new EncryptedConverter(protector));

            builder
                .Entity<Pt_Documento>()
                .Property(e => e.NomeDocumento)
                .HasConversion(new EncryptedConverter(protector));

            builder
                .Entity<Pt_Documento>()
                .Property(e => e.ExtensaoDocumento)
                .HasConversion(new EncryptedConverter(protector));

            builder
                .Entity<Pt_Documento>()
                .Property(e => e.TamanhoDocumento)
                .HasConversion(new EncryptedConverter(protector));

            builder
                .Entity<Pt_Documento>()
                .Property(e => e.ReferenciaDocumento)
                .HasConversion(new EncryptedConverter(protector));

            builder
                .Entity<Pt_Licenca>()
                .Property(e => e.SerialNumber)
                .HasConversion(new EncryptedConverter(protector));

            builder
                .Entity<Pt_TipoLicensa>()
                .Property(e => e.NomeTipoLicensa)
                .HasConversion(new EncryptedConverter(protector));

            builder
                .Entity<Pt_TipoLicensa>()
                .Property(e => e.Publico)
                .HasConversion(new EncryptedConverter(protector));

            builder
                .Entity<Pt_TipoLicensa>()
                .Property(e => e.BeneficiosLicensa)
                .HasConversion(new EncryptedConverter(protector));

            builder
                .Entity<Pt_TipoLicensa>()
                .Property(e => e.NumeroUsuarios)
                .HasConversion(new EncryptedConverter(protector));

            builder
                .Entity<Pt_Usuario>()
                .Property(e => e.TipoPagamento)
                .HasConversion(new EncryptedConverter(protector));
            #endregion

            #region EncryptedDataIntOrDecimal
            builder
                .Entity<Pt_ControlePrograma>()
                .Property(e => e.TempoAtivo)
                .HasConversion(new EncryptedIntConverter(protector));

            builder
               .Entity<Pt_FimTrabalho>()
               .Property(e => e.HoraTrabalho)
               .HasConversion(new EncryptedIntConverter(protector));

            builder
               .Entity<Pt_FimTrabalho>()
               .Property(e => e.Remuneracao)
               .HasConversion(new EncryptedDecimalConverter(protector));

            builder
               .Entity<ModelBase>()
               .Property(e => e.ID)
               .HasConversion(new EncryptedIntConverter(protector));

            builder
              .Entity<Pt_TipoLicensa>()
              .Property(e => e.Preco)
              .HasConversion(new EncryptedDecimalConverter(protector));

            builder
             .Entity<Pt_UserCompany>()
             .Property(e => e.CompanyId)
             .HasConversion(new EncryptedDecimalConverter(protector));

            builder
             .Entity<Pt_UserCompany>()
             .Property(e => e.UserId)
             .HasConversion(new EncryptedDecimalConverter(protector));

            builder
             .Entity<Pt_Usuario>()
             .Property(e => e.Pagamento)
             .HasConversion(new EncryptedDecimalConverter(protector));
            #endregion

            #region EncryptedDate
            builder
                .Entity<Pt_Licenca>()
                .Property(e => e.ExpirateDate)
                .HasConversion(new EncryptedDateConverter(protector));

            builder
                .Entity<ModelBase>()
                .Property(e => e.DataCriacao)
                .HasConversion(new EncryptedDateConverter(protector));

            builder
                .Entity<ModelBase>()
                .Property(e => e.DataAtualizacao)
                .HasConversion(new EncryptedDateConverter(protector));

            builder
                .Entity<ModelBase>()
                .Property(e => e.DataRemocao)
                .HasConversion(new EncryptedDateConverter(protector));
            #endregion

            #region ConfigurationsClass
            builder.ApplyConfiguration(new Pt_CaminhoPastaConfigurations());
            builder.ApplyConfiguration(new Pt_ControleProgramaConfigurations());
            builder.ApplyConfiguration(new Pt_EmpresaConfigurations());
            builder.ApplyConfiguration(new Pt_LicensaConfigurations());
            builder.ApplyConfiguration(new Pt_TaskConfigurations());
            builder.ApplyConfiguration(new Pt_UsuarioConfigurations());
            #endregion

            #region Tipo Licença
            builder.Entity<Pt_TipoLicensa>()
                .HasData(new Pt_TipoLicensa { ID = 1, NomeTipoLicensa = "Opal", BeneficiosLicensa = "Grátis", Preco = 0, Publico = "Funcionários + Freelancers", NumeroUsuarios = "1" });
            builder.Entity<Pt_TipoLicensa>()
                .HasData(new Pt_TipoLicensa { ID = 2, NomeTipoLicensa = "Rubi", BeneficiosLicensa = "Premium + Anúncios", Preco = 2500, Publico = "Funcionários + Freelancers", NumeroUsuarios = "1" });
            builder.Entity<Pt_TipoLicensa>()
                .HasData(new Pt_TipoLicensa { ID = 3, NomeTipoLicensa = "Esmeralda", BeneficiosLicensa = "Premium", Preco = 7550, Publico = "Funcionários + Freelancers", NumeroUsuarios = "1" });

            builder.Entity<Pt_TipoLicensa>()
                .HasData(new Pt_TipoLicensa { ID = 4, NomeTipoLicensa = "Rubi", BeneficiosLicensa = "Premium + Anúncios", Preco = 6250, Publico = "Micro Empresa", NumeroUsuarios = "1 - 5" });
            builder.Entity<Pt_TipoLicensa>()
                .HasData(new Pt_TipoLicensa { ID = 5, NomeTipoLicensa = "Rubi", BeneficiosLicensa = "Premium + Anúncios", Preco = 16250, Publico = "Pequena Empresa", NumeroUsuarios = "6 - 14" });
            builder.Entity<Pt_TipoLicensa>()
                .HasData(new Pt_TipoLicensa { ID = 6, NomeTipoLicensa = "Rubi", BeneficiosLicensa = "Premium + Anúncios", Preco = 26250, Publico = "Média empresa", NumeroUsuarios = "15 - 34" });
            builder.Entity<Pt_TipoLicensa>()
                .HasData(new Pt_TipoLicensa { ID = 7, NomeTipoLicensa = "Rubi", BeneficiosLicensa = "Premium + Anúncios", Preco = 46250, Publico = "Grande empresa", NumeroUsuarios = "+35" });

            builder.Entity<Pt_TipoLicensa>()
                .HasData(new Pt_TipoLicensa { ID = 8, NomeTipoLicensa = "Esmeralda", BeneficiosLicensa = "Premium", Preco = 14500, Publico = "Micro Empresa", NumeroUsuarios = "1 - 5" });
            builder.Entity<Pt_TipoLicensa>()
                .HasData(new Pt_TipoLicensa { ID = 9, NomeTipoLicensa = "Esmeralda", BeneficiosLicensa = "Premium", Preco = 35500, Publico = "Pequena Empresa", NumeroUsuarios = "6 - 14" });
            builder.Entity<Pt_TipoLicensa>()
                .HasData(new Pt_TipoLicensa { ID = 10, NomeTipoLicensa = "Esmeralda", BeneficiosLicensa = "Premium", Preco = 55500, Publico = "Média empresa", NumeroUsuarios = "15 - 34" });
            builder.Entity<Pt_TipoLicensa>()
                .HasData(new Pt_TipoLicensa { ID = 11, NomeTipoLicensa = "Esmeralda", BeneficiosLicensa = "Premium", Preco = 95000, Publico = "Grande empresa", NumeroUsuarios = "+35" });
            #endregion

            #region Profissões
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 1, NomeProfissao = "Analista de Crédito" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 2, NomeProfissao = "Analista de Dados" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 3, NomeProfissao = "Analista de Investimentos" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 4, NomeProfissao = "Analista de Mercado Financeiro" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 5, NomeProfissao = "Analista de Planejamento Financeiro" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 6, NomeProfissao = "Analista Financeiro" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 7, NomeProfissao = "Arquiteto" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 8, NomeProfissao = "Consultor de TI" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 9, NomeProfissao = "Consultor Financeiro" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 10, NomeProfissao = "Contabilista / Contador" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 11, NomeProfissao = "Controller Financeiro" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 12, NomeProfissao = "Corretor de Valores" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 13, NomeProfissao = "Cientista de Dados" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 14, NomeProfissao = "Designer de UX/UI" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 15, NomeProfissao = "Designer Gráfico" });
            builder.Entity<Pt_ProfissaoUsuario>()
               .HasData(new Pt_ProfissaoUsuario { ID = 16, NomeProfissao = "Desenvolvedor de Jogos" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 17, NomeProfissao = "Desenvolvedor de Software" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 18, NomeProfissao = "Engenheiro Civil" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 19, NomeProfissao = "Engenheiro de Dados" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 20, NomeProfissao = "Engenheiro de Software" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 21, NomeProfissao = "Especialista em E-commerce" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 22, NomeProfissao = "Especialista em SEO" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 23, NomeProfissao = "Gestor de Carteira" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 24, NomeProfissao = "Gestor de Fundos de Investimento" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 25, NomeProfissao = "Gestor de Projetos de TI" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 26, NomeProfissao = "Produtor de Jogos" });
            builder.Entity<Pt_ProfissaoUsuario>()
               .HasData(new Pt_ProfissaoUsuario { ID = 27, NomeProfissao = "Programador de Computador" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 28, NomeProfissao = "Programador de Jogos" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 29, NomeProfissao = "Profissional de Marketing Digital" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 30, NomeProfissao = "Profissional de Recursos Humanos" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 31, NomeProfissao = "Profissional de Saúde" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 32, NomeProfissao = "Professor" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 33, NomeProfissao = "Suporte Técnico" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 34, NomeProfissao = "Web Designer" });
            builder.Entity<Pt_ProfissaoUsuario>()
                .HasData(new Pt_ProfissaoUsuario { ID = 35, NomeProfissao = "Web Developer" });

            #endregion
        }
    }
}
