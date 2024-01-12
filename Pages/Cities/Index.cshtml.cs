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
using System.Security.Policy;

namespace Cursuri.Pages.Cities
{
    public class IndexModel : PageModel
    {
        private readonly Cursuri.Data.CursuriContext _context;

        public IndexModel(Cursuri.Data.CursuriContext context)
        {
            _context = context;
        }

        public IList<City> City { get;set; } = default!;
        public CityIndexData CityData { get; set; }
        public int CityID { get; set; }
        public int CourseID { get; set; }
        public async Task OnGetAsync(int? id, int? courseID)
        {
            CityData = new CityIndexData();
            CityData.Cities = await _context.City
            .Include(i => i.Courses)
            .ThenInclude(c => c.Professor)
            .OrderBy(i => i.CityName)
            .ToListAsync();
            if (id != null)
            {
                CityID = id.Value;
                City city = CityData.Cities
                .Where(i => i.ID == id.Value).Single();
                CityData.Courses = city.Courses; ;
            }
        }
    }
}
