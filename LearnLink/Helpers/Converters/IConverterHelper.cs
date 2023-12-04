using LearnLink.Data.Entities;
using LearnLink.Models.Absences;
using LearnLink.Models.Classes;
using LearnLink.Models.Courses;
using LearnLink.Models.Disciplines;
using LearnLink.Models.Evaluations;
using System.Linq;

namespace LearnLink.Helpers.Converters
{
    public interface IConverterHelper
    {
        CoursesViewModel CourseToCoursesViewModel(Course course);

        IQueryable<CoursesViewModel> CoursesToCoursesViewModels(IQueryable<Course> courses);

        DisciplinesViewModel DisciplineToDisciplinesViewModel(Discipline discipline);

        IQueryable<DisciplinesViewModel> DisciplinesToDisciplinesViewModels(IQueryable<Discipline> disciplines);

        ClassesViewModel ClassToClassesViewModel(Class clas);

        IQueryable<ClassesViewModel> ClassesToClassesViewModels(IQueryable<Class> classes);

        RegisterClassViewModel ClassToRegisterClassViewModel(Class clas);

        AbsenceDisciplinesViewModel AbsenceStudentsToDisciplinesViewModel(AbsenceStudentsViewModel model);

        EvaluationDisciplinesViewModel EvaluationStudentsToDisciplinesViewModel(EvaluationStudentsViewModel model);
    }
}
