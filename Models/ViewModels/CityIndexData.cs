using System.Security.Policy;

namespace Cursuri.Models.ViewModels
{
    public class CityIndexData
    {
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
