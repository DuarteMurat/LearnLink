using LearnLink.Models;
using LearnLink.Repositories.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLink.Data.Entities
{
    public interface IReportRepository : IGenericRepository<Report>
    {
        Task<bool> IsReportsEmptyAsync();

        Task<IQueryable<ReportsViewModel>> GetAllReportsWithUsersAsync();

        Task<ReportsViewModel> GetReportByIdWithUserAsync(int Id);

        Task<IQueryable<ReportsViewModel>> GetAllReportsByUserAsync(string userId);
    }
}
