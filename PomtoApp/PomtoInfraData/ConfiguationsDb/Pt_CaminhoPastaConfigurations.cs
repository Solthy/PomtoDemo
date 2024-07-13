using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PomtoDomain.Model;

namespace PomtoInfraData.ConfiguationsDb
{
    public class Pt_CaminhoPastaConfigurations : IEntityTypeConfiguration<Pt_CaminhoPasta>
    {
        public void Configure(EntityTypeBuilder<Pt_CaminhoPasta> builder)
        {
            builder
                 .HasOne(b => b.Usuario)
                 .WithMany(b => b.CaminhoPastas)
                 .HasForeignKey(b => b.UsuarioID);
        }
    }
}
