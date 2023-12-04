using LearnLink.Data.Entities;
using LearnLink.Models.API.Students;
using LearnLink.Models.ClassStudents;
using LearnLink.Repositories.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLink.Data.ClassStudents
{
    public interface IClassStudentRepository : IGenericRepository<ClassStudent>
    {
        Task<bool> IsClassStudentsEmptyAsync();

        Task<IQueryable<string>> GetStudentsIdsByClassIdAsync(int classId);

        Task<IEnumerable<ClassStudentsViewModel>> GetClassStudentsListAsync(IQueryable<string> studentsIds);

        Task<IQueryable<StudentsSelectable>> GetAllStudentsSelectableAsync(int classId);

        Task<ClassStudent> GetClassStudentAsync(int classId, string studentId);

        Task<int> GetClassStudentsTotalAsync(int classId);

        Task<IQueryable<StudentViewModel>> GetStudentsByClassCodeAsync(string classCode);
    }
}
