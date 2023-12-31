﻿using LearnLink.Data.Classes;
using LearnLink.Data.ClassStudents;
using LearnLink.Data.Disciplines;
using LearnLink.Data.Entities;
using LearnLink.Data.Evaluations;
using LearnLink.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolWeb.Data.Absences;
using SchoolWeb.Data.CourseDisciplines;
using SchoolWeb.Data.Courses;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLink.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IGenderRepository _genderRepository;
        private readonly IQualificationRepository _qualificationRepository;
        private readonly IReportRepository _reportRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly ICourseDisciplineRepository _courseDisciplineRepository;
        private readonly IClassRepository _classRepository;
        private readonly IClassStudentRepository _classStudentRepository;
        private readonly IAbsenceRepository _absenceRepository;
        private readonly IEvaluationRepository _evaluationRepository;

        public SeedDb
            (
                DataContext context,
                IUserHelper userHelper,
                IGenderRepository genderRepository,
                IQualificationRepository qualificationRepository,
                IReportRepository reportRepository,
                ICourseRepository courseRepository,
                IDisciplineRepository disciplineRepository,
                ICourseDisciplineRepository courseDisciplinesRepository,
                IClassRepository classRepository,
                IClassStudentRepository classStudentRepository,
                IAbsenceRepository absenceRepository,
                IEvaluationRepository evaluationRepository
            )
        {
            _context = context;
            _userHelper = userHelper;
            _genderRepository = genderRepository;
            _qualificationRepository = qualificationRepository;
            _reportRepository = reportRepository;
            _courseRepository = courseRepository;
            _disciplineRepository = disciplineRepository;
            _courseDisciplineRepository = courseDisciplinesRepository;
            _classRepository = classRepository;
            _classStudentRepository = classStudentRepository;
            _absenceRepository = absenceRepository;
            _evaluationRepository = evaluationRepository;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            await AddRolesAsync();
            await AddGendersAsync();
            await AddQualificationsAsync();
            await AddConfigurations();
            await AddUserAdminAsync();
            await AddUserStaffAsync();
            await AddUsersStudentsAsync();
            await AddReportsAsync();
            await AddCoursesAsync();
            await AddDisciplinesAsync();
            await AddCourseDisciplinesAsync();
            await AddClassesAsync();
            await AddClassStudentsAsync();
            await AddAbsencesAsync();
            await AddEvaluationsAsync();
        }

        private async Task AddRolesAsync()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Staff");
            await _userHelper.CheckRoleAsync("Student");
        }

        private async Task AddGendersAsync()
        {
            await _genderRepository.AddGenderAsync("Male");
            await _genderRepository.AddGenderAsync("Female");
            await _genderRepository.AddGenderAsync("Not Specified");
        }

        private async Task AddQualificationsAsync()
        {
            await _qualificationRepository.AddQualificationAsync("Entry Level");
            await _qualificationRepository.AddQualificationAsync("Level 1");
            await _qualificationRepository.AddQualificationAsync("Level 2");
            await _qualificationRepository.AddQualificationAsync("Level 3");
            await _qualificationRepository.AddQualificationAsync("Level 4");
            await _qualificationRepository.AddQualificationAsync("Level 5");
            await _qualificationRepository.AddQualificationAsync("Level 6");
            await _qualificationRepository.AddQualificationAsync("Level 7");
            await _qualificationRepository.AddQualificationAsync("Level 8");
        }

        private async Task AddConfigurations()
        {
            var configurations = await _context.Configurations.FirstOrDefaultAsync();

            if (configurations == null)
            {
                var configuration = new Configuration
                {
                    ClassMaxStudents = 35,
                    MaxPercentageAbsence = 7
                };

                await _context.Configurations.AddAsync(configuration);
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddUserAdminAsync()
        {
            var userAdmin = await _userHelper.GetUserByEmailAsync("admin@gmail.com");

            if (userAdmin == null)
            {
                userAdmin = new User
                {
                    UserName = "admin@gmail.com",
                    FirstName = "Duarte",
                    LastName = "Marques",
                    GenderId = _context.Genders.Where(x => x.Name == "Male").FirstOrDefault().Id,
                    QualificationId = _context.Qualifications.Where(x => x.Name == "Level 7").FirstOrDefault().Id,
                    CcNumber = "1234567890",
                    BirthDate = DateTime.Today.AddYears(-35),
                    Address = "Mem-Martins,Sintra",
                    City = "Lisbon",
                    PhoneNumber = "+351964088000",
                    Email = "admin@gmail.com",
                    PasswordChanged = true
                };

                await AddUserWithRoleAsync(userAdmin, "Admin");
            }

            await IsUserInRole(userAdmin, "Admin");
        }

        private async Task AddUserStaffAsync()
        {
            var userStaff = await _userHelper.GetUserByEmailAsync("staff@gmail.com");

            if (userStaff == null)
            {
                userStaff = new User
                {
                    UserName = "staff@gmail.com",
                    FirstName = "Catarina",
                    LastName = "Furtado",
                    GenderId = _context.Genders.Where(x => x.Name == "Female").FirstOrDefault().Id,
                    QualificationId = _context.Qualifications.Where(x => x.Name == "Level 6").FirstOrDefault().Id,
                    CcNumber = "4567890123",
                    BirthDate = DateTime.Today.AddYears(-30),
                    Address = "Foz do Douro,10",
                    City = "Porto",
                    PhoneNumber = "+351987654321",
                    Email = "staff@gmail.com",
                    PasswordChanged = true
                };

                await AddUserWithRoleAsync(userStaff, "Staff");
            }

            await IsUserInRole(userStaff, "Staff");
        }

        private async Task AddUsersStudentsAsync()
        {
            await AddUserStudent1Async();
            await AddUserStudent2Async();
            await AddUserStudent3Async();
        }

        private async Task AddUserStudent1Async()
        {
            var userStudent = await _userHelper.GetUserByEmailAsync("student1@gmail.com");

            if (userStudent == null)
            {
                userStudent = new User
                {
                    UserName = "student1@gmail.com",
                    FirstName = "Dario",
                    LastName = "Diaz",
                    GenderId = _context.Genders.Where(x => x.Name == "Not Specified").FirstOrDefault().Id,
                    QualificationId = _context.Qualifications.Where(x => x.Name == "Entry Level").FirstOrDefault().Id,
                    CcNumber = "7890123456",
                    BirthDate = DateTime.Today.AddYears(-20),
                    Address = "Rua do Ouro, 19",
                    City = "Lisbon",
                    PhoneNumber = "+351210789123",
                    Email = "student1@gmail.com",
                    PasswordChanged = true
                };

                await AddUserWithRoleAsync(userStudent, "Student");
            }

            await IsUserInRole(userStudent, "Student");
        }

        private async Task AddUserStudent2Async()
        {
            var userStudent = await _userHelper.GetUserByEmailAsync("student2@gmail.com");

            if (userStudent == null)
            {
                userStudent = new User
                {
                    UserName = "student2@gmail.com",
                    FirstName = "John",
                    LastName = "Murphy",
                    GenderId = _context.Genders.Where(x => x.Name == "Male").FirstOrDefault().Id,
                    QualificationId = _context.Qualifications.Where(x => x.Name == "Level 3").FirstOrDefault().Id,
                    CcNumber = "6860123456",
                    BirthDate = DateTime.Today.AddYears(-25),
                    Address = "Rua da Constituicao, 59",
                    City = "Porto",
                    PhoneNumber = "+351658789123",
                    Email = "student2@gmail.com",
                    PasswordChanged = true
                };

                await AddUserWithRoleAsync(userStudent, "Student");
            }

            await IsUserInRole(userStudent, "Student");
        }

        private async Task AddUserStudent3Async()
        {
            var userStudent = await _userHelper.GetUserByEmailAsync("student3@gmail.com");

            if (userStudent == null)
            {
                userStudent = new User
                {
                    UserName = "student3@gmail.com",
                    FirstName = "Raquel",
                    LastName = "Henriques",
                    GenderId = _context.Genders.Where(x => x.Name == "Female").FirstOrDefault().Id,
                    QualificationId = _context.Qualifications.Where(x => x.Name == "Level 4").FirstOrDefault().Id,
                    CcNumber = "6777123456",
                    BirthDate = DateTime.Today.AddYears(-22),
                    Address = "Rua do Outeiro, 2",
                    City = "Faro",
                    PhoneNumber = "+351658789777",
                    Email = "student3@gmail.com",
                    PasswordChanged = true
                };

                await AddUserWithRoleAsync(userStudent, "Student");
            }

            await IsUserInRole(userStudent, "Student");
        }

        private async Task AddUserWithRoleAsync(User user, string role)
        {
            var result = await _userHelper.AddUserAsync(user, "123456");

            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException($"Seeding could not create {role} user");
            }

            await _userHelper.AddUserToRoleAsync(user, role);

            var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            await _userHelper.ConfirmEmailAsync(user, myToken);
        }

        private async Task IsUserInRole(User user, string role)
        {
            var isUserInRole = await _userHelper.IsUserInRoleAsync(user, role);

            if (!isUserInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, role);
            }
        }

        private async Task AddReportsAsync()
        {
            var userStaff = await _userHelper.GetUserByEmailAsync("staff@gmail.com");

            if (userStaff != null)
            {
                if (await _reportRepository.IsReportsEmptyAsync())
                {
                    Report report1 = new Report
                    {
                        UserId = userStaff.Id,
                        Title = "Add Sql Server Discipline",
                        Message = "Please add this discipline as soon as possible. Thank you.",
                        Date = DateTime.Today.AddDays(-6),
                        Solved = true,
                        SolvedDate = DateTime.Today.AddDays(-1)
                    };

                    Report report2 = new Report
                    {
                        UserId = userStaff.Id,
                        Title = "Add Python Course",
                        Message = "Please add this course as soon as possible. Thank you.",
                        Date = DateTime.Today.AddDays(-4),
                        Solved = false
                    };

                    Report report3 = new Report
                    {
                        UserId = userStaff.Id,
                        Title = "Bug When Changing User",
                        Message = "When changing users it logs into the older one. Please help.",
                        Date = DateTime.Today.AddDays(-2),
                        Solved = false
                    };

                    await _reportRepository.CreateAsync(report1);
                    await _reportRepository.CreateAsync(report2);
                    await _reportRepository.CreateAsync(report3);
                }
            }
        }

        private async Task AddCoursesAsync()
        {
            if (await _courseRepository.IsCoursesEmptyAsync())
            {
                Course course1 = new Course
                {
                    Code = "PROALGO",
                    Name = "Programming Algorithms",
                    Area = "Forms Development",
                    Duration = 250
                };

                Course course2 = new Course
                {
                    Code = "IISTIL",
                    Name = "Integration of information systems – technologies and integration levels",
                    Area = "Programming",
                    Duration = 600
                };

                Course course3 = new Course
                {
                    Code = "MATIS",
                    Name = "Mobile access to information systems",
                    Area = "Mobile Development",
                    Duration = 350
                };

                await _courseRepository.CreateAsync(course1);
                await _courseRepository.CreateAsync(course2);
                await _courseRepository.CreateAsync(course3);
            }
        }

        private async Task AddDisciplinesAsync()
        {
            if (await _disciplineRepository.IsDisciplinesEmptyAsync())
            {
                Discipline discipline1 = new Discipline
                {
                    Code = "ANETCMVCD",
                    Name = "ASP.NET Core Web App MVC Development",
                    Area = "Web Development",
                    Duration = 400
                };

                Discipline discipline2 = new Discipline
                {
                    Code = "HAJD",
                    Name = "HTML and JavaScript Development",
                    Area = "Frontend Development",
                    Duration = 300
                };

                Discipline discipline3 = new Discipline
                {
                    Code = "DATASEC",
                    Name = "Data Security And Infrastructure",
                    Area = "Business Technology",
                    Duration = 125
                };

                await _disciplineRepository.CreateAsync(discipline1);
                await _disciplineRepository.CreateAsync(discipline2);
                await _disciplineRepository.CreateAsync(discipline3);
            }
        }

        private async Task AddCourseDisciplinesAsync()
        {
            if (await _courseDisciplineRepository.IsCourseDisciplinesEmptyAsync())
            {
                var course1 = await _courseRepository.GetByCodeAsync("IISTIL");
                var course2 = await _courseRepository.GetByCodeAsync("MATIS");

                var discicline1 = await _disciplineRepository.GetByCodeAsync("ANETCMVCD");
                var discicline2 = await _disciplineRepository.GetByCodeAsync("HAJD");
                var discicline3 = await _disciplineRepository.GetByCodeAsync("DATASEC");

                if (course1 != null && discicline1 != null)
                {
                    CourseDiscipline courseDiscipline1 = new CourseDiscipline
                    {
                        CourseId = course1.Id,
                        DisciplineId = discicline1.Id
                    };

                    await _courseDisciplineRepository.CreateAsync(courseDiscipline1);
                }

                if (course1 != null && discicline2 != null)
                {
                    CourseDiscipline courseDiscipline2 = new CourseDiscipline
                    {
                        CourseId = course1.Id,
                        DisciplineId = discicline2.Id
                    };

                    await _courseDisciplineRepository.CreateAsync(courseDiscipline2);
                }

                if (course2 != null && discicline3 != null)
                {
                    CourseDiscipline courseDiscipline3 = new CourseDiscipline
                    {
                        CourseId = course2.Id,
                        DisciplineId = discicline3.Id
                    };

                    await _courseDisciplineRepository.CreateAsync(courseDiscipline3);
                }
            }
        }

        private async Task AddClassesAsync()
        {
            if (await _classRepository.IsClassesEmptyAsync())
            {
                var course1 = await _courseRepository.GetByCodeAsync("PROALGO");
                var course2 = await _courseRepository.GetByCodeAsync("HAJD");
                var course3 = await _courseRepository.GetByCodeAsync("DATASEC");

                if (course1 != null)
                {
                    Class clas1 = new Class
                    {
                        Code = "TCD003",
                        Name = "Technological Course Day",
                        CourseId = course1.Id,
                        StartDate = DateTime.Today.AddDays(-100),
                        EndDate = DateTime.Today.AddDays(200)
                    };

                    await _classRepository.CreateAsync(clas1);
                }

                if (course2 != null)
                {
                    Class clas2 = new Class
                    {
                        Code = "BCD016",
                        Name = "Business Course Day",
                        CourseId = course2.Id,
                        StartDate = DateTime.Today.AddDays(-50),
                        EndDate = DateTime.Today.AddDays(150)
                    };

                    await _classRepository.CreateAsync(clas2);
                }

                if (course3 != null)
                {
                    Class clas3 = new Class
                    {
                        Code = "TCN067",
                        Name = "Technological Course Night",
                        CourseId = course3.Id,
                        StartDate = DateTime.Today.AddDays(-5),
                        EndDate = DateTime.Today.AddDays(295)
                    };

                    await _classRepository.CreateAsync(clas3);
                }
            }
        }

        private async Task AddClassStudentsAsync()
        {
            if (await _classStudentRepository.IsClassStudentsEmptyAsync())
            {
                var class1 = await _classRepository.GetByCodeAsync("TCD003");
                var class2 = await _classRepository.GetByCodeAsync("BCD016");

                var userStudent1 = await _userHelper.GetUserByEmailAsync("student1@gmail.com");
                var userStudent2 = await _userHelper.GetUserByEmailAsync("student2@gmail.com");
                var userStudent3 = await _userHelper.GetUserByEmailAsync("student3@gmail.com");

                if (class1 != null && userStudent1 != null)
                {
                    ClassStudent classStudent1 = new ClassStudent
                    {
                        ClassId = class1.Id,
                        UserId = userStudent1.Id
                    };

                    await _classStudentRepository.CreateAsync(classStudent1);
                }

                if (class1 != null && userStudent2 != null)
                {
                    ClassStudent classStudent2 = new ClassStudent
                    {
                        ClassId = class1.Id,
                        UserId = userStudent2.Id
                    };

                    await _classStudentRepository.CreateAsync(classStudent2);
                }

                if (class2 != null && userStudent3 != null)
                {
                    ClassStudent classStudent3 = new ClassStudent
                    {
                        ClassId = class2.Id,
                        UserId = userStudent3.Id
                    };

                    await _classStudentRepository.CreateAsync(classStudent3);
                }
            }
        }

        private async Task AddAbsencesAsync()
        {
            if (await _absenceRepository.IsAbsencesEmptyAsync())
            {
                var userStudent1 = await _userHelper.GetUserByEmailAsync("student1@gmail.com");
                var class1 = await _classRepository.GetByCodeAsync("TCD003");
                var discicline1 = await _disciplineRepository.GetByCodeAsync("ANETCMVCD");

                if (userStudent1 != null && class1 != null && discicline1 != null)
                {
                    Absence absence1 = new Absence
                    {
                        UserId = userStudent1.Id,
                        ClassId = class1.Id,
                        DisciplineId = discicline1.Id,
                        Date = DateTime.Today.AddDays(-3),
                        Duration = 4
                    };

                    await _absenceRepository.CreateAsync(absence1);

                    Absence absence2 = new Absence
                    {
                        UserId = userStudent1.Id,
                        ClassId = class1.Id,
                        DisciplineId = discicline1.Id,
                        Date = DateTime.Today.AddDays(-1),
                        Duration = 2
                    };

                    await _absenceRepository.CreateAsync(absence2);
                }
            }
        }

        private async Task AddEvaluationsAsync()
        {
            if (await _evaluationRepository.IsEvaluationsEmptyAsync())
            {
                var userStudent1 = await _userHelper.GetUserByEmailAsync("student1@gmail.com");
                var class1 = await _classRepository.GetByCodeAsync("TCD003");
                var discicline1 = await _disciplineRepository.GetByCodeAsync("ANETCMVCD");
                var discicline2 = await _disciplineRepository.GetByCodeAsync("HAJD");

                if (userStudent1 != null && class1 != null && discicline1 != null && discicline2 != null)
                {
                    Evaluation evaluation1 = new Evaluation
                    {
                        UserId = userStudent1.Id,
                        ClassId = class1.Id,
                        DisciplineId = discicline1.Id,
                        Date = DateTime.Today.AddDays(-3),
                        Grade = 16
                    };

                    await _evaluationRepository.CreateAsync(evaluation1);

                    Evaluation evaluation2 = new Evaluation
                    {
                        UserId = userStudent1.Id,
                        ClassId = class1.Id,
                        DisciplineId = discicline2.Id,
                        Date = DateTime.Today.AddDays(-1),
                        Grade = 8
                    };

                    await _evaluationRepository.CreateAsync(evaluation2);
                }
            }
        }
    }
}
