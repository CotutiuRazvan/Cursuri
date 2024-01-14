using System.ComponentModel.DataAnnotations;

namespace Cursuri.Models
{
    public class Enrolling
    {
        public int ID { get; set; }
        public int? MemberID { get; set; }
        public Member? Member { get; set; }
        public int? CourseID { get; set; }
        public Course? Course { get; set; }
        [DataType(DataType.Date)]
        public DateTime EnrolmentDate { get; set; }
    }
}
