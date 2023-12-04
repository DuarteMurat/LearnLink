using LearnLink.Data.Entities;
using System;

namespace LearnLink.Models
{
    public class ReportsViewModel : Report
    {
        public string Age
        {
            get
            {
                return $"{(DateTime.Today - Date).Days} Days";
            }
        }
    }
}
