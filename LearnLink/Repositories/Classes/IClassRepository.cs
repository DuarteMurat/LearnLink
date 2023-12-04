using LearnLink.Data.Entities;
using LearnLink.Models.Home;
using LearnLink.Repositories.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLink.Data.Classes
{
    public interface IClassRepository : IGenericRepository<Class>
    {
        Task<bool> IsClassesEmptyAsync();

        Task<bool> IsCodeInUseOnRegisterAsync(string code);

        Task<bool> IsCodeInUseOnEditAsync(int idClass, string code);

        Task<Class> GetByCodeAsync(string code);

        Task<IEnumerable<SelectListItem>> GetComboClassesAsync();

        Task<IQueryable<HomeClassViewModel>> GetHomeClassesAsync();
    }
}
