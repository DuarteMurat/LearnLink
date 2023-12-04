using System.Collections.Generic;

namespace LearnLink.Models.Home
{
    public class HomeCoursesClassesViewModel
    {
        public IEnumerable<HomeCourseViewModel> Courses { get; set; }

        public IEnumerable<HomeClassViewModel> Classes { get; set; }
    }
}
