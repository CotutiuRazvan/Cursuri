using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cursuri.Data;
using Cursuri.Models;
using System.Net;

namespace Cursuri.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly Cursuri.Data.CursuriContext _context;

        public IndexModel(Cursuri.Data.CursuriContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get;set; } = default!;
        public CourseData CourseD { get; set; }
        public int CourseID { get; set; }
        public int GradeID { get; set; }
        public string TitleSort { get; set; }
        public string ProfessorSort { get; set; }
        public string CurrentFilter { get; set; }
        public async Task OnGetAsync(int? id, int? gradeID, string sortOrder, string searchString)
        {
            CourseD = new CourseData();

            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ProfessorSort = sortOrder == "professor" ? "professor_desc" : "professor";

            CurrentFilter = searchString;

            CourseD.Courses = await _context.Course
                    .Include(b => b.City)
                    .Include(b => b.Professor)
                    .Include(b => b.CourseGrades)
                    .ThenInclude(b => b.Grade)
                    .AsNoTracking()
                    .OrderBy(b => b.Title)
                    .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                CourseD.Courses = CourseD.Courses.Where(s => s.Professor.FirstName.Contains(searchString)
                                    || s.Professor.LastName.Contains(searchString)
                                    || s.Title.Contains(searchString));
            }
            if (id != null)
            {
                CourseID = id.Value;
                Course course = CourseD.Courses
                .Where(i => i.ID == id.Value).Single();
                CourseD.Grades = course.CourseGrades.Select(s => s.Grade);
            }
            switch (sortOrder)
            {
                case "title_desc":
                    CourseD.Courses = CourseD.Courses.OrderByDescending(s =>
                   s.Title);
                    break;
                case "professor_desc":
                    CourseD.Courses = CourseD.Courses.OrderByDescending(s =>
                   s.Professor.FullName);
                    break;
                case "professor":
                    CourseD.Courses = CourseD.Courses.OrderBy(s =>
                   s.Professor.FullName);
                    break;
                default:
                    CourseD.Courses = CourseD.Courses.OrderBy(s => s.Title);
                    break;
            }
        }
    }
}
