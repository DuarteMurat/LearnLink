using System.Collections.Generic;

namespace LearnLink.Models.Evaluations
{
    public class StudentEvaluationsCourseDetailsViewModel
    {
        public string CourseName { get; set; }

        public IEnumerable<StudentEvaluationDisciplines> Disciplines { get; set; }
    }
}
