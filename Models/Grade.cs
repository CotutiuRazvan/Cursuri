namespace Cursuri.Models
{
    public class Grade
    {
        public int ID { get; set; }
        public string GradeType { get; set; }
        public ICollection<CourseGrade>? CourseGrades { get; set; }
    }
}
