using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PomtoDomain.Model;

namespace PomtoInfraData.ConfiguationsDb
{
    public class Pt_ControleProgramaConfigurations : IEntityTypeConfiguration<Pt_ControlePrograma>
    {
        public void Configure(EntityTypeBuilder<Pt_ControlePrograma> builder)
        {
            builder
                .HasOne(b => b.Usuario)
                .WithMany(b => b.ControleProgramas)
                .HasForeignKey(b => b.UsuarioID);
        }
    }
}