using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentGradeSystem.Models;

public class Course
{
    public Course()
    {
        Assignments = new HashSet<Assignments>();
    }


    [Key]
    [Column(name:"CourseId")]
    public int Id { get; set; }
    
    public string CourseName { get; set; } = "Network Scripting";

    public int FinalGrade { get; set; }
    
    [ForeignKey("Student")]
    public int StudentId { get; set; }
    
    


    public ICollection<Assignments> Assignments { get; set; }

    public int GetCalculatedGrade
    {
        get
        {
            float finalWeight = 0;
            foreach (var obj in Assignments)
            {
                finalWeight += obj.WeightOfGrade;
            }
            
            return Assignments.Sum(assignment => (int)(assignment.Grade * assignment.WeightOfGrade/ finalWeight));
        }
    }

    
    
}