using Microsoft.AspNetCore.Mvc;
using StudentGradeSystem.Data;
using StudentGradeSystem.Models;

namespace StudentGradeSystem.Controllers;

public class AssignmentController : Controller
{
    private readonly ApplicationDbContext _db;

    public AssignmentController(ApplicationDbContext db)
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

        var courseFromDB = _db.Courses.Find(id);
        courseFromDB.Assignments = _db.Assignment.Where(x => x.CourseId == id).ToList();

        if (courseFromDB.GetCalculatedGrade != 0)
        {
            courseFromDB.FinalGrade = courseFromDB.GetCalculatedGrade;
            _db.Courses.Update(courseFromDB);
            _db.SaveChanges();
        }
        
        return View(courseFromDB);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Assignments assignmentsObj, int? courseId)
    {
        if (courseId is null or 0)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            _db.Assignment.Add(assignmentsObj);
            _db.SaveChanges();
            return RedirectToAction("Index", new {id = courseId.ToString()});
        }
       
        return View();
    }

    public IActionResult Delete(int? id, int courseId)
    {
        if (id is null or 0 || courseId == 0)
        {
            return NotFound();
        }

        var courseFromDB = _db.Courses.Find(courseId);
        courseFromDB.Assignments = _db.Assignment.Where(x => x.CourseId == courseId).ToList();
        foreach (var obj in courseFromDB.Assignments)
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
    public IActionResult Delete(Assignments assignmentsObj)
    {
        var refId = assignmentsObj.CourseId;
        if (ModelState.IsValid)
        {
            _db.Assignment.Remove(assignmentsObj);
            _db.SaveChanges();
            return RedirectToAction("Index", new {id = refId.ToString()});
        }

        return View(assignmentsObj);
    }

    
    public int FinalCalculatedGrade(int? id)
    {
        if (id is null or 0)
        {
            return 0;
        }

        int finalCalculatedGrade = 0;
        var listOfAssignmentGrades = _db.Courses.Find(id);
        if (listOfAssignmentGrades.Assignments == null || listOfAssignmentGrades.Assignments.Count == 0)
        {
            return finalCalculatedGrade;
        }
        foreach (var assignment in listOfAssignmentGrades.Assignments)
        {
            var grade = assignment.Grade;
            var weight = assignment.WeightOfGrade / 100;
            finalCalculatedGrade += (int)(grade * weight);

        }

        return finalCalculatedGrade;
    }
}


