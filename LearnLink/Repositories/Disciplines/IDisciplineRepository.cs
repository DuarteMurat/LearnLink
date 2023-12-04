using LearnLink.Data.Entities;
using LearnLink.Models.Home;
using LearnLink.Repositories.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLink.Data.Disciplines
{
    public interface IDisciplineRepository : IGenericRepository<Discipline>
    {
        Task<bool> IsDisciplinesEmptyAsync();

        Task<bool> IsCodeInUseOnRegisterAsync(string code);

        Task<bool> IsCodeInUseOnEditAsync(int idDiscipline, string code);

        Task<Discipline> GetByCodeAsync(string code);

        Task<IEnumerable<SelectListItem>> GetComboDisciplinesInCourseAsync(int courseId);

        Task<IQueryable<HomeDisciplineViewModel>> GetHomeDisciplinesInCourseAsync(int courseId);
    }
}
