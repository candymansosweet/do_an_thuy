using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ProjectMemConfiguration : IEntityTypeConfiguration<ProjectMem>
    {
        public void Configure(EntityTypeBuilder<ProjectMem> builder)
        {
            builder.HasOne(pm => pm.Project)
                .WithMany()
                .HasForeignKey(pm => pm.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pm => pm.Staff)
                .WithMany(s => s.ProjectMems)
                .HasForeignKey(pm => pm.StaffId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pm => pm.AppointedBy)
                .WithMany(s => s.AppointedProjectMems)
                .HasForeignKey(pm => pm.AppointedById)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
} 