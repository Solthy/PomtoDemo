using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PomtoDomain.Model;

namespace PomtoInfraData.ConfiguationsDb
{
    public class Pt_TaskConfigurations : IEntityTypeConfiguration<Pt_Task>
    {
        public void Configure(EntityTypeBuilder<Pt_Task> builder)
        {
            builder
               .HasOne(b => b.Usuario)
               .WithMany(b => b.Tasks)
               .HasForeignKey(b => b.UsuarioRemetenteID);

            builder
              .HasMany(b => b.TaskReceives)
              .WithOne(b => b.Task)
              .HasForeignKey(b => b.TaskID);
        }
    }
}