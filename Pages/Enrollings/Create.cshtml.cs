using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cursuri.Data;
using Cursuri.Models;

namespace Cursuri.Pages.Enrollings
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
        ViewData["CourseID"] = new SelectList(_context.Course, "ID", "ID");
        ViewData["MemberID"] = new SelectList(_context.Member, "ID", "ID");
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
