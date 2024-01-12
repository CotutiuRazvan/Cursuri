using Microsoft.AspNetCore.Mvc.RazorPages;
using Cursuri.Data;

namespace Cursuri.Models
{
    public class CourseGradesPageModel : PageModel
    {
        public List<AssignedGradeData> AssignedGradeDataList;
        public void PopulateAssignedGradeData(CursuriContext context, Course course)
        {
            var allGrades = context.Grade;
            var courseGrades = new HashSet<int>(course.CourseGrades.Select(c => c.GradeID)); //
            AssignedGradeDataList = new List<AssignedGradeData>();
            foreach (var cat in allGrades)
            {
                AssignedGradeDataList.Add(new AssignedGradeData
                {
                    GradeID = cat.ID,
                    Name = cat.GradeType,
                    Assigned = courseGrades.Contains(cat.ID)
                });
            }
        }
        public void UpdateCourseGrades(CursuriContext context, string[] selectedGrades, Course courseToUpdate)
        {
            if (selectedGrades == null)
            {
                courseToUpdate.CourseGrades = new List<CourseGrade>();
                return;
            }
            var selectedGradesHS = new HashSet<string>(selectedGrades);
            var courseGrades = new HashSet<int>
                (courseToUpdate.CourseGrades.Select(c => c.Grade.ID));
            foreach (var cat in context.Grade)
            {
                if (selectedGradesHS.Contains(cat.ID.ToString()))
                {
                    if (!courseGrades.Contains(cat.ID))
                    {
                        courseToUpdate.CourseGrades.Add(
                            new CourseGrade
                            {
                                CourseID = courseToUpdate.ID,
                                GradeID = cat.ID
                            });
                    }
                }
                else
                {
                    if (courseGrades.Contains(cat.ID))
                    {
                        CourseGrade courseToRemove
                            = courseToUpdate
                                .CourseGrades
                                .SingleOrDefault(i => i.GradeID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
