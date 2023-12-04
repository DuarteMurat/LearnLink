using System.ComponentModel.DataAnnotations;

namespace LearnLink.Models.ClassStudents
{
    public class StudentsSelectable : ClassStudentsViewModel
    {
        [Display(Name = "Selected")]
        public bool IsSelected { get; set; }
    }
}
