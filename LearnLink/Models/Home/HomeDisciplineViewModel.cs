﻿using System.ComponentModel.DataAnnotations;

namespace LearnLink.Models.Home
{
    public class HomeDisciplineViewModel
    {
        public string Name { get; set; }

        public string Area { get; set; }

        [Display(Name = "Hours")]
        public int Duration { get; set; }
    }
}
