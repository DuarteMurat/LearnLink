using LearnLink.Data.Entities;
using System.Collections.Generic;

namespace LearnLink.Models.CourseDisciplines
{
    public class CourseDisciplinesViewModel : Course
    {
        public string CodeName
        {
            get
            {
                if (!string.IsNullOrEmpty(Code) && !string.IsNullOrEmpty(Name))
                {
                    return $"{Code}  |  {Name}";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public IEnumerable<Discipline> Disciplines { get; set; }
    }
}
