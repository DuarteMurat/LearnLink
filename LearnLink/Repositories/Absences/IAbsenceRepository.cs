using LearnLink.Data.Entities;
using LearnLink.Models.Absences;
using LearnLink.Repositories.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWeb.Data.Absences
{
    public interface IAbsenceRepository : IGenericRepository<Absence>
    {
        Task<bool> IsAbsencesEmptyAsync();

        Task<IQueryable<StudentAbsence>> GetClassStudentAbsencesAsync(int classId, int disciplineId);
    }
}
