using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cursuri.Data;
using Microsoft.EntityFrameworkCore;
using Cursuri.Models;

namespace Cursuri.Pages.Enrollments
{
    public class CreateModel : PageModel
    {
        private readonly Cursuri.Data.CursuriContext _context;

        public CreateModel(Cursuri.Data.CursuriContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var courseList = _context.Course
            .Include(b => b.Professor)
            .Select(x => new
            {
                x.ID,
                CourseFullName = x.Title + " - " + x.Professor.LastName + " " + x.Professor.FirstName
            });
            ViewData["CourseID"] = new SelectList(courseList, "ID", "CourseFullName");
            ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
            return Page();
        }

        [BindProperty]
        public Enrolling Enrolling { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Enrolling == null || Enrolling == null)
            {
                return Page();
            }

            _context.Enrolling.Add(Enrolling);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
