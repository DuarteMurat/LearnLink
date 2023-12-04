using LearnLink.Data.Entities;
using LearnLink.Repositories.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnLink.Data
{
    public interface IQualificationRepository : IGenericRepository<Qualification>
    {
        Task<Qualification> GetQualificationByNameAsync(string name);

        Task AddQualificationAsync(string name);

        IEnumerable<SelectListItem> GetComboQualifications();
    }
}
