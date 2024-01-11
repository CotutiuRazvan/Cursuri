﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cursuri.Data;
using Cursuri.Models;

namespace Cursuri.Pages.Professors
{
    public class DeleteModel : PageModel
    {
        private readonly Cursuri.Data.CursuriContext _context;

        public DeleteModel(Cursuri.Data.CursuriContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Professor Professor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Professor == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor.FirstOrDefaultAsync(m => m.ID == id);

            if (professor == null)
            {
                return NotFound();
            }
            else 
            {
                Professor = professor;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Professor == null)
            {
                return NotFound();
            }
            var professor = await _context.Professor.FindAsync(id);

            if (professor != null)
            {
                Professor = professor;
                _context.Professor.Remove(Professor);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
