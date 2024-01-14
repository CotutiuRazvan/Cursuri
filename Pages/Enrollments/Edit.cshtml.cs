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

namespace Cursuri.Pages.Enrollments
{
    public class EditModel : PageModel
    {
        private readonly Cursuri.Data.CursuriContext _context;

        public EditModel(Cursuri.Data.CursuriContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Enrolling Enrolling { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Enrolling == null)
            {
                return NotFound();
            }

            var enrolling =  await _context.Enrolling.FirstOrDefaultAsync(m => m.ID == id);
            if (enrolling == null)
            {
                return NotFound();
            }
            Enrolling = enrolling;

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Enrolling).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollingExists(Enrolling.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EnrollingExists(int id)
        {
          return (_context.Enrolling?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
