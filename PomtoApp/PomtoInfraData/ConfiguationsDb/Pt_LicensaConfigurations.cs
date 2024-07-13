using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PomtoDomain.Model;

namespace PomtoInfraData.ConfiguationsDb
{
    public class Pt_LicensaConfigurations : IEntityTypeConfiguration<Pt_Licenca>
    {
        public void Configure(EntityTypeBuilder<Pt_Licenca> builder)
        {
            builder
                .HasOne(b => b.Usuario)
                .WithMany(b => b.Licencas)
                .HasForeignKey(b => b.UsuarioID);

            builder
                .HasOne(b => b.Empresa)
                .WithMany(b => b.Licencas)
                .HasForeignKey(b => b.EmpresaID);

            builder
                .HasOne(b => b.TipoLicensa)
                .WithMany(b => b.Licencas)
                .HasForeignKey(b => b.TipoLicensaID);

            builder
              .HasIndex(b => b.SerialNumber)
              .HasDatabaseName("IX_Licencas_SerialNumber")
              .IsUnique(true);
        }
    }
}