namespace Cursuri.Models
{
    public class City
    {
        public int ID { get; set; }
        public string? CityName { get; set; }
        public ICollection<Course>? Courses { get; set; }
    }
}
