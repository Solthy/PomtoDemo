using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PomtoDomain.Model;

namespace PomtoInfraData.ConfiguationsDb
{
    public class Pt_UsuarioConfigurations : IEntityTypeConfiguration<Pt_Usuario>
    {
        public void Configure(EntityTypeBuilder<Pt_Usuario> builder)
        {
            builder
              .HasMany(b => b.TelefoneUsuarios)
              .WithOne(b => b.Usuario)
              .HasForeignKey(b => b.UsuarioID);

            builder
             .HasMany(b => b.FimTrabalhos)
             .WithOne(b => b.Usuario)
             .HasForeignKey(b => b.UsuarioID);

            builder
             .HasMany(b => b.Logins)
             .WithOne(b => b.Usuario)
             .HasForeignKey(b => b.UserId);

            builder
             .HasOne(b => b.ProfissaoUsuario)
             .WithOne(b => b.Usuario)
             .HasForeignKey<Pt_Usuario>(b => b.ProfissaoID);

            builder
               .HasMany(b => b.TaskReceives)
               .WithOne(b => b.Usuario)
               .HasForeignKey(b => b.UsuarioReceptorID);


            builder
               .HasMany(b => b.Documentos)
               .WithOne(b => b.Usuario)
               .HasForeignKey(b => b.UsuarioId);

            builder
              .HasMany(b => b.Empresas)
              .WithOne(b => b.Usuario)
              .HasForeignKey(b => b.UserId);

            builder
              .HasIndex(b => b.ProfissaoID)
              .HasDatabaseName("IX_Usuarios_ProfissaoID")
              .IsUnique(false);
        }
    }
}