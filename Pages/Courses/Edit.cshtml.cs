using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cursuri.Data;
using Cursuri.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cursuri.Pages.Courses
{
    [Authorize(Roles = "Admin")]

    public class EditModel : CourseGradesPageModel
    {
        private readonly Cursuri.Data.CursuriContext _context;

        public EditModel(Cursuri.Data.CursuriContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            Course = await _context.Course
                .Include(b => b.City)
                .Include(b => b.CourseGrades).ThenInclude(b => b.Grade)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Course == null)
            {
                return NotFound();
            }
            PopulateAssignedGradeData(_context, Course);
            var professorList = _context.Professor.Select(x => new
            {
                x.ID,
                FullName = x.LastName + " " + x.FirstName
            });

            ViewData["CityID"] = new SelectList(_context.City, "ID", "CityName");
            ViewData["ProfessorID"] = new SelectList(professorList, "ID", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedGrades)
        {
            if (id == null)
            {
                return NotFound();
            }
            //se va include Author conform cu sarcina de la lab 2
            var courseToUpdate = await _context.Course
            .Include(i => i.City)
            .Include(i => i.CourseGrades)
            .ThenInclude(i => i.Grade)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (courseToUpdate == null)
            {
                return NotFound();
            }
            //se va modifica AuthorID conform cu sarcina de la lab 2
            if (await TryUpdateModelAsync<Course>(
            courseToUpdate,
            "Course",
            i => i.Title, i => i.Professor,
            i => i.Price, i => i.StartingDate, i => i.CityID))
            {
                UpdateCourseGrades(_context, selectedGrades, courseToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdateCourseGrades(_context, selectedGrades, courseToUpdate);
            PopulateAssignedGradeData(_context, courseToUpdate);
            return Page();
        }
    }
}
