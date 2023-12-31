﻿using LearnLink.Data.Entities;
using LearnLink.Repositories.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLink.Data
{
    public class GenderRepository : GenericRepository<Gender>, IGenderRepository
    {
        private readonly DataContext _context;

        public GenderRepository(DataContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Gender> GetGenderByNameAsync(string name)
        {
            return await _context.Genders.Where(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task AddGenderAsync(string name)
        {
            var gender = await this.GetGenderByNameAsync(name);

            if (gender != null)
            {
                return;
            }

            await _context.Genders.AddAsync(new Gender { Name = name });
            await _context.SaveChangesAsync();
        }

        public IEnumerable<SelectListItem> GetComboGenders()
        {
            var list = _context.Genders.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select gender...)",
                Value = "0"
            });

            return list;
        }
    }
}
