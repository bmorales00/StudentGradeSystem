using Microsoft.EntityFrameworkCore;
using StudentGradeSystem.Models;

namespace StudentGradeSystem.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Student> Student{ get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Assignments> Assignment { get; set; }
    
}