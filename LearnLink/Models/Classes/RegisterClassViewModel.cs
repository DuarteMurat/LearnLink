using LearnLink.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace LearnLink.Models.Classes
{
    public class RegisterClassViewModel : Class
    {
        public IEnumerable<SelectListItem> Courses { get; set; }
    }
}
