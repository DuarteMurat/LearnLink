﻿using LearnLink.Data;
using LearnLink.Data.Entities;
using LearnLink.Models.Evaluations;
using LearnLink.Repositories.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLink.Data.Evaluations
{
    public class EvaluationRepository : GenericRepository<Evaluation>, IEvaluationRepository
    {
        private readonly DataContext _context;
        private readonly IConfigurationRepository _configurationRepository;

        public EvaluationRepository(DataContext context, IConfigurationRepository configurationRepository)
            : base(context)
        {
            _context = context;
            _configurationRepository = configurationRepository;
        }

        public async Task<bool> IsEvaluationsEmptyAsync()
        {
            return await _context.Evaluations.FirstOrDefaultAsync() == null ? true : false;
        }

        public async Task<Evaluation> GetEvaluationAsync(string userId, int classId, int disciplineId)
        {
            return await _context.Evaluations
                .Where(x => x.UserId == userId && x.ClassId == classId && x.DisciplineId == disciplineId)
                .FirstOrDefaultAsync();
        }

        public async Task<IList<StudentsEvaluationIndexViewModel>> GetStudentEvaluationsIndexAsync()
        {
            var students = new List<StudentsEvaluationIndexViewModel>();

            await Task.Run(() =>
            {
                students =
                 (
                     from user in _context.Users
                     join userRole in _context.UserRoles
                     on user.Id equals userRole.UserId
                     join role in _context.Roles
                     on userRole.RoleId equals role.Id
                     where role.Name == "Student"
                     orderby user.FirstName
                     select new
                     {
                         UserId = user.Id,
                         FirstName = user.FirstName,
                         LastName = user.LastName,
                         ProfilePicturePath = user.ProfilePicturePath
                     }
                 )
                 .Select(x => new StudentsEvaluationIndexViewModel
                 {
                     UserId = x.UserId,
                     FirstName = x.FirstName,
                     LastName = x.LastName,
                     ProfilePicturePath = x.ProfilePicturePath
                 }).ToList();
            });

            return students;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCoursesByStudentAsync(string userId)
        {
            var courses = Enumerable.Empty<SelectListItem>();

            await Task.Run(() =>
            {
                courses =
                (
                    from course in _context.Courses
                    join clas in _context.Classes
                    on course.Id equals clas.CourseId
                    join classStudent in _context.ClassStudents
                    on clas.Id equals classStudent.ClassId
                    where classStudent.UserId == userId
                    orderby course.Name
                    select new
                    {
                        CourseId = course.Id,
                        Name = course.Name
                    }
                ).Select(x => new SelectListItem
                {
                    Value = x.CourseId.ToString(),
                    Text = x.Name
                });
            });

            return courses;
        }

        public async Task<IEnumerable<EvaluationViewModel>> GetStudentEvaluationsByCourseAsync(string userId, int courseId)
        {
            var evaluations = Enumerable.Empty<EvaluationViewModel>();

            await Task.Run(() =>
            {
                evaluations =
                (
                    from evaluation in _context.Evaluations
                    join discipline in _context.Disciplines
                    on evaluation.DisciplineId equals discipline.Id
                    join courseDiscipline in _context.CourseDisciplines
                    on discipline.Id equals courseDiscipline.DisciplineId
                    join course in _context.Courses
                    on courseDiscipline.CourseId equals course.Id
                    where evaluation.UserId == userId && course.Id == courseId
                    orderby discipline.Name
                    select new
                    {
                        DisciplineCode = discipline.Code,
                        DisciplineName = discipline.Name,
                        Date = evaluation.Date,
                        Grade = evaluation.Grade
                    }
                ).Select(x => new EvaluationViewModel
                {
                    DisciplineCode = x.DisciplineCode,
                    DisciplineName = x.DisciplineName,
                    Date = x.Date,
                    Grade = x.Grade
                });
            });

            return evaluations;
        }

        public async Task<IQueryable<StudentEvaluation>> GetClassStudentEvaluationsAsync(int classId, int disciplineId)
        {
            var students = Enumerable.Empty<StudentEvaluation>().AsQueryable();

            await Task.Run(() =>
            {
                students =
                (
                    from user in _context.Users
                    join userRole in _context.UserRoles
                    on user.Id equals userRole.UserId
                    join role in _context.Roles
                    on userRole.RoleId equals role.Id
                    join classStudent in _context.ClassStudents
                    on user.Id equals classStudent.UserId
                    where role.Name == "Student" && classStudent.ClassId == classId
                    orderby user.FirstName
                    select new
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        ProfilePicturePath = user.ProfilePicturePath,
                        Evaluation = (
                            from evaluation in _context.Evaluations
                            where evaluation.UserId == user.Id && evaluation.ClassId == classId && evaluation.DisciplineId == disciplineId
                            select new
                            {
                                Date = evaluation.Date,
                                Grade = evaluation.Grade
                            }).First()
                    }
                ).Select(x => new StudentEvaluation
                {
                    UserId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ProfilePicturePath = x.ProfilePicturePath,
                    EvaluationDate = x.Evaluation.Date,
                    Grade = x.Evaluation.Grade
                });
                ;
            });

            return students;
        }

        public async Task<IQueryable<StudentEvaluationDisciplines>> GetStudentEvaluationDisciplinesByCourseAsync(string userId, int courseId)
        {
            var configuration = await _configurationRepository.GetConfigurationsAsync();

            var evaluations = _context.Evaluations
                .Where(x => x.UserId == userId && x.Class.Course.Id == courseId)
                .Select(evaluation => new StudentEvaluationDisciplines
                {
                    Code = evaluation.Discipline.Code,
                    Name = evaluation.Discipline.Name,
                    Duration = evaluation.Discipline.Duration,
                    Date = evaluation.Date,
                    Grade = evaluation.Grade,
                });

            return evaluations;
        }

        private static int CalculatePercentage(int hoursDiscipline, int hoursAbsence)
        {
            if (hoursDiscipline == 0 && hoursAbsence == 0)
            {
                return 0;
            }

            double total = Convert.ToDouble(hoursDiscipline);
            double partial = Convert.ToDouble(hoursAbsence);
            double percentage = 100 / (total / partial);

            return Convert.ToInt32(percentage);
        }
    }
}
