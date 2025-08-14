using Training_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Training_Management_System.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("Grades");

            builder.HasKey(s => s.id);

            builder.Property(s => s.Value)
                .IsRequired();

            builder.HasCheckConstraint("CK_Grade_Value", "[Value] BETWEEN 0 AND 100");
            //To Check the Value in the range or not after insert it into DB note"CK : mean Check Constraint take it like pk or fr"

        }
    }
}
