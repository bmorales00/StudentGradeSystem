using Microsoft.AspNetCore.Mvc;
using StudentGradeSystem.Data;
using StudentGradeSystem.Models;

namespace StudentGradeSystem.Controllers;

public class CourseController : Controller
{
    private readonly ApplicationDbContext _db;

    public CourseController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    // GET
    public IActionResult Index(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }
        
        var studentFromDB = _db.Student.Find(id);
        studentFromDB.Courses = _db.Courses.Where(x => x.StudentId == id).ToList();
        
        return View(studentFromDB);
    }
    

    public IActionResult Create()
    {
        return View();
    }
    
  
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Course courseObj, int? studentId)
    {
        if (studentId is null or 0)
        {
            return NotFound();
        }

        //var studentFromDB = _db.Student.Find(studentId);
        
        if (ModelState.IsValid)
        {
            _db.Courses.Add(courseObj);
          
            _db.SaveChanges();
            return RedirectToAction("Index", new {id = studentId.ToString()});
        }
        return View();
    }
    
    public IActionResult Delete(int? id, int studentId)
    {
        if (id is null or 0 || studentId == 0 )
        {
            return NotFound();
        }
        var studentFromDB = _db.Student.Find(studentId);
        studentFromDB.Courses = _db.Courses.Where(x => x.StudentId == studentId).ToList(); 
        foreach (var obj in studentFromDB.Courses)
        {
            if (obj.Id == id)
            {
                
                return View(obj);
            }
        }

        return NotFound();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Course courseObj)
    {
        var refId = courseObj.StudentId;
        if (ModelState.IsValid)
        {
            _db.Courses.Remove(courseObj);
            _db.SaveChanges();
            return RedirectToAction("Index", new{id = refId.ToString()});
        }
        
        return View(courseObj);
    }
    
}