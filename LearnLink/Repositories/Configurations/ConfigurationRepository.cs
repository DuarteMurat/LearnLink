﻿using LearnLink.Data;
using LearnLink.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SchoolWeb.Data
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly DataContext _context;

        public ConfigurationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Configuration> GetConfigurationsAsync()
        {
            return await _context.Configurations.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveConfigurationsAsync(int maxStudents, int maxPercentAbsence)
        {
            bool isSuccess = false;

            var configurations = await GetConfigurationsAsync();

            if (configurations != null)
            {
                configurations.ClassMaxStudents = maxStudents;
                configurations.MaxPercentageAbsence = maxPercentAbsence;

                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    isSuccess = true;
                }
            }

            return isSuccess;
        }
    }
}
