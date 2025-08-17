using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Training_Management_System.Models;
namespace Training_Management_System.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(s => s.id);

            builder.Property(c => c.Name)
                .IsRequired().HasMaxLength(50);

            builder.Property(c => c.Category)
                .IsRequired();

            builder.HasIndex(c => c.Name)
                .IsUnique();

            builder.HasMany(c => c.Sessions)
                .WithOne(s => s.course)
                .HasForeignKey(c => c.courseid)
                .OnDelete(DeleteBehavior.Restrict) ;//the relation between course and session
        }
    }
}
