using LearnLink.Data.Entities;
using LearnLink.Models.Evaluations;
using LearnLink.Repositories.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLink.Data.Evaluations
{
    public interface IEvaluationRepository : IGenericRepository<Evaluation>
    {
        Task<bool> IsEvaluationsEmptyAsync();

        Task<Evaluation> GetEvaluationAsync(string userId, int classId, int disciplineId);

        Task<IList<StudentsEvaluationIndexViewModel>> GetStudentEvaluationsIndexAsync();

        Task<IEnumerable<SelectListItem>> GetComboCoursesByStudentAsync(string userId);

        Task<IEnumerable<EvaluationViewModel>> GetStudentEvaluationsByCourseAsync(string userId, int courseId);

        Task<IQueryable<StudentEvaluation>> GetClassStudentEvaluationsAsync(int classId, int disciplineId);

        Task<IQueryable<StudentEvaluationDisciplines>> GetStudentEvaluationDisciplinesByCourseAsync(string userId, int courseId);
    }
}
