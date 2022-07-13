using Microsoft.AspNetCore.Mvc;
using StudentGradeSystem.Data;
using StudentGradeSystem.Models;

namespace StudentGradeSystem.Controllers;

public class StudentController : Controller
{
    private readonly ApplicationDbContext _db;

    public StudentController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    // GET
    public IActionResult Index()
    {
        IEnumerable<Student> studentObj = _db.Student;
        return View(studentObj);
    }

    //GET
    public IActionResult Create()
    {
        
        return View();
    }
    
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Student studentObj)
    {
        
        if (ModelState.IsValid)
        {
            studentObj.Courses = new List<Course>();
            _db.Student.Add(studentObj);
            _db.SaveChanges();
            return RedirectToAction("Index");
            
        }

        return View(studentObj);

    }
    
    //GET
    public IActionResult Edit(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }

        var studentFromDb = _db.Student.Find(id);
        
        if (studentFromDb == null)
        {
            return NotFound();
        }
        return View(studentFromDb);
    }
    
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Student studentObj)
    {
        if (studentObj.FirstName == studentObj.LastName)
        {
            ModelState.AddModelError("Name", "The entered first name is the same as the last name");
        }
        
        //
        if (ModelState.IsValid)
        {

            // _db.Entry(studentObj).State = EntityState.Modified;
            _db.Student.Update(studentObj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(studentObj);
        
    }
    
    //GET
    public IActionResult Delete(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }

        var studentFromDb = _db.Student.Find(id);
        
        if (studentFromDb == null)
        {
            return NotFound();
        }
        return View(studentFromDb);
    }
    
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Student studentObj)
    {
        //
        if (ModelState.IsValid)
        {

            // _db.Entry(studentObj).State = EntityState.Modified;
            _db.Student.Remove(studentObj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(studentObj);
        
    }
    
}