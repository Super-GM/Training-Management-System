using Microsoft.EntityFrameworkCore;
using Training_Management_System.Configurations;
using Training_Management_System.Models;
namespace Training_Management_System.Data
{
    public class SystemContext : DbContext
    {
        public DbSet<Course> courses { get; set; }
        public DbSet<Session> sessions { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Grade> grades { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=TrainingManagement;Integrated Security=True;Trust Server Certificate=True"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new SessionConfiguration());
            modelBuilder.ApplyConfiguration(new GradeConfiguration());

            modelBuilder.Entity<User>().HasData(
                new User { id=1, Name="Mohamed Adel",Email="mohamed@gmail.com",Role="instructor"},
                new User { id=2,Name="Moheb",Email="Moheb@gmail.com",Role="Trainer"},
                new User { id=3,Name="Omar",Email="Omar@gmail.com",Role="instructor"}
                );
            modelBuilder.Entity<Course>().HasData(
                new Course {id=1, Name="ASP MVC" ,Category=CourseCategory.Programming,instructorid=1 },
                new Course { id=2,Name="Biochemistry",Category=CourseCategory.Chemistry,instructorid=3}
                );
            modelBuilder.Entity<Session>().HasData(
                new Session {id = 1, StartDate=new DateTime(2025,8,16),EndDate=new DateTime(2025,8,20),courseid=1 },
                new Session { id=2 ,StartDate=new DateTime(2025,8,15),EndDate=new DateTime(2025,8,21),courseid=2}
                );
            modelBuilder.Entity<Grade>().HasData(
                new Grade { id =1,Sessionid=1,Traineeid=2,Value=90}
                );
        }
    }
}
