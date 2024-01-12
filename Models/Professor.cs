using System.ComponentModel.DataAnnotations;

namespace Cursuri.Models
{
    public class Professor
    {
        public int ID { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public ICollection<Course>? Courses { get; set; }
    }
}
