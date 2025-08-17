using Training_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Training_Management_System.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Sessions");

            builder.HasKey(s => s.id);

            builder.Property(c => c.StartDate)
                .IsRequired();

            builder.Property(c => c.EndDate)
                .IsRequired();

            builder.HasCheckConstraint("CK_Session_EndDate", "[EndDate] > [StartDate]");
            builder.HasCheckConstraint("ck_Session_StartDate", "[StartDate] > GETDATE()");

            builder.HasMany(s => s.grades)
                .WithOne(s => s.session)
                .HasForeignKey(s => s.Sessionid)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
