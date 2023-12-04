using LearnLink.Data.Entities;
using LearnLink.Repositories.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnLink.Data
{
    public interface IGenderRepository : IGenericRepository<Gender>
    {
        Task<Gender> GetGenderByNameAsync(string name);

        Task AddGenderAsync(string name);

        IEnumerable<SelectListItem> GetComboGenders();
    }
}
