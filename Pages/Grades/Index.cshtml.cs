using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cursuri.Data;
using Cursuri.Models;
using Cursuri.Models.ViewModels;

namespace Cursuri.Pages.Grades
{
    public class IndexModel : PageModel
    {
        private readonly Cursuri.Data.CursuriContext _context;

        public IndexModel(Cursuri.Data.CursuriContext context)
        {
            _context = context;
        }

        public IList<Grade> Grade { get;set; } = default!;
        public GradeIndexData GradeData { get; set; }
        public int GradeID { get; set; }
        public int CourseID { get; set; }
        public async Task OnGetAsync(int? id, int? bookID)
        {
            GradeData = new GradeIndexData();
            GradeData.Grades = await _context.Grade
                .Include(i => i.CourseGrades)
                    .ThenInclude(i => i.Course)
                    .ThenInclude(c => c.Professor)
                .OrderBy(i => i.GradeType)
                .ToListAsync();
            if (id != null)
            {
                GradeID = id.Value;
                Grade grade = GradeData.Grades
                    .Where(i => i.ID == id.Value).Single();
                GradeData.CourseGrades = grade.CourseGrades;
            }
        }
    }
}
