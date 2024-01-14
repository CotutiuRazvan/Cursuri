using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cursuri.Data;
using Cursuri.Models;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;

namespace Cursuri.Pages.Courses
{
    [Authorize(Roles = "Admin")]

    public class CreateModel : CourseGradesPageModel
    {
        private readonly Cursuri.Data.CursuriContext _context;

        public CreateModel(Cursuri.Data.CursuriContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var professorList = _context.Professor.Select(x => new
            {
                x.ID,
                FullName = x.LastName + " " + x.FirstName
            });
            ViewData["CityID"] = new SelectList(_context.City, "ID", "CityName");
            ViewData["ProfessorID"] = new SelectList(professorList, "ID", "FullName");

            var course = new Course();
            course.CourseGrades = new List<CourseGrade>();
            PopulateAssignedGradeData(_context, course);

            return Page();
        }

        [BindProperty]
        public Course Course { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync(string[] selectedGrades)
        {
            var newCourse = new Course();
            if (selectedGrades != null)
            {
                newCourse.CourseGrades = new List<CourseGrade>();
                foreach (var cat in selectedGrades)
                {
                    var catToAdd = new CourseGrade
                    {
                        GradeID = int.Parse(cat)
                    };
                    newCourse.CourseGrades.Add(catToAdd);
                }
            }
            Course.CourseGrades = newCourse.CourseGrades;
            _context.Course.Add(Course);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
