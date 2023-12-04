using LearnLink.Data.Entities;
using LearnLink.Models.CourseDisciplines;
using LearnLink.Repositories.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWeb.Data.CourseDisciplines
{
    public interface ICourseDisciplineRepository : IGenericRepository<CourseDiscipline>
    {
        Task<bool> IsCourseDisciplinesEmptyAsync();

        Task<IQueryable<Discipline>> GetDisciplinesByCourseIdAsync(int courseId);

        Task<IQueryable<DisciplineSelectable>> GetAllDisciplinesSelectableAsync(int courseId);

        Task<CourseDiscipline> GetCourseDisciplineAsync(int courseId, int disciplineId);
    }
}
