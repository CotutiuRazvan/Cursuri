namespace Cursuri.Models
{
    public class CourseGrade
    {
        public int ID { get; set; }
        public int CourseID { get; set; }
        public Course Course { get; set; }
        public int GradeID { get; set; }
        public Grade Grade { get; set; }
    }
}
