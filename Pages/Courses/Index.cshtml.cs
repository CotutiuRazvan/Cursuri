using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cursuri.Data;
using Cursuri.Models;

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

        public async Task OnGetAsync()
        {
            if (_context.Course != null)
            {
                Course = await _context.Course
                    .Include(b => b.Professor)
                    .Include(b => b.City)
                    .ToListAsync();
            }
        }
    }
}
