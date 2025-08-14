using Training_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
                .IsUnique();//to make the Name unique

            builder.HasMany(c => c.Sessions)
                .WithOne(s => s.course)
                .HasForeignKey(c => c.courseid)
                .OnDelete(DeleteBehavior.Restrict) ;//the relation between course and session
        }
    }
}
