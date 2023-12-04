using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearnLink.Models.CourseDisciplines
{
    public class CourseDisciplinesSelectableViewModel
    {
        [Required]
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        [Required]
        public IList<DisciplineSelectable> DisciplinesSelectable { get; set; }
    }
}
