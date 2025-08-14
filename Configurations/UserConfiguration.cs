using Training_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Training_Management_System.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(s => s.id);

            builder.Property(s => s.Name)
                .IsRequired().HasMaxLength(50);

            builder.Property(s => s.Email)
                .IsRequired().HasMaxLength(100);

            builder.Property(s => s.Role)
                .IsRequired();

            builder.HasMany(s => s.courses)
                .WithOne(c => c.instructor)
                .HasForeignKey(s => s.instructorid)
                 .OnDelete(DeleteBehavior.Restrict);//relation between user and course

            builder.HasMany(s => s.grades)
                .WithOne(c => c.Trainee)
                .HasForeignKey(s => s.Traineeid)
                .OnDelete(DeleteBehavior.Restrict) ;//relation between user and grade
        }

    }
}
