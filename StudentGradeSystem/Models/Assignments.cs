using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentGradeSystem.Models;

public class Assignments
{
    [Key]
    [Column(name:"AssignmentId")]
    public int Id { get; set; }

    [Required]
    public string AssignmentName { get; set; }
    
    [Required]
    public int Grade { get; set; }

    [Required]
    public float WeightOfGrade { get; set; }
    
    [ForeignKey("Course")]
    public int CourseId { get; set; }
    
}