using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace Cursuri.Models
{
    public class Course
    {
        public int ID { get; set; }

        [Display(Name = "Cursul")]
        public string? Title { get; set; }

        [Display(Name = "Pretul")]
        [Column(TypeName = "decimal(0.01, 500)")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartingDate { get; set; }

        [ForeignKey("City")]
        public int? CityID { get; set; }
        public City? City { get; set; }

        [Display(Name = "Profesorul")]
        [ForeignKey("Professor")]
        public int? ProfessorID { get; set; }
        public Professor? Professor { get; set; }
       
        public ICollection<CourseGrade>? CourseGrades { get; set; }
    }
}
