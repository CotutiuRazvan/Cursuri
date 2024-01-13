namespace Cursuri.Models.ViewModels
{
    public class GradeIndexData
    {
        public IEnumerable<Grade> Grades { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<CourseGrade> CourseGrades { get; set; }
    }
}
