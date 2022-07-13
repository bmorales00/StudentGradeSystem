using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentGradeSystem.Models;
public class Student
{
    public Student()
    {
        this.Courses = new HashSet<Course>();
    }

    [Key]
    [Column(name:"StudentId")]
    public int Id { get; set; }
    
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    
    
    //Navigation Property
    public ICollection<Course> Courses { get; set; }
    

}