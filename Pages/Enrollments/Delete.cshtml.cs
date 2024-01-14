using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cursuri.Data;
using Cursuri.Models;

namespace Cursuri.Pages.Enrollments
{
    public class DeleteModel : PageModel
    {
        private readonly Cursuri.Data.CursuriContext _context;

        public DeleteModel(Cursuri.Data.CursuriContext context)
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

            var enrolling = await _context.Enrolling
                .Include(b => b.Member)
                .Include(b => b.Course)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (enrolling == null)
            {
                return NotFound();
            }
            else 
            {
                Enrolling = enrolling;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Enrolling == null)
            {
                return NotFound();
            }
            var enrolling = await _context.Enrolling.FindAsync(id);

            if (enrolling != null)
            {
                Enrolling = enrolling;
                _context.Enrolling.Remove(Enrolling);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
