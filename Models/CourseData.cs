namespace Cursuri.Models
{
    public class CourseData
    {
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Grade> Grades { get; set; }
        public IEnumerable<CourseGrade> CourseGrades { get; set; }
    }
}
