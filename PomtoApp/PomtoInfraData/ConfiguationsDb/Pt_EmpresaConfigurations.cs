using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PomtoDomain.Model;

namespace PomtoInfraData.ConfiguationsDb
{
    public class Pt_EmpresaConfigurations : IEntityTypeConfiguration<Pt_Empresa>
    {
        public void Configure(EntityTypeBuilder<Pt_Empresa> builder)
        {
            builder
              .HasMany(b => b.Usuarios)
              .WithOne(b => b.Empresa)
              .HasForeignKey(b => b.CompanyId);

            builder
                .HasMany(b => b.Logins)
                .WithOne(b => b.Empresa)
                .HasForeignKey(b => b.CompanyId);

            builder
              .HasMany(b => b.TelefoneEmpresas)
              .WithOne(b => b.Empresa)
              .HasForeignKey(b => b.EmpresaID);

            builder
                .HasIndex(b => b.NIF)
                .HasDatabaseName("IX_Empresas_NIF")
                .IsUnique(true);
        }
    }
}