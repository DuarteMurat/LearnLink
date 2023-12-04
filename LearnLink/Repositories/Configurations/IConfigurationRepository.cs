using LearnLink.Data.Entities;
using System.Threading.Tasks;

namespace SchoolWeb.Data
{
    public interface IConfigurationRepository
    {
        Task<Configuration> GetConfigurationsAsync();

        Task<bool> SaveConfigurationsAsync(int maxStudents, int maxPercentAbsence);
    }
}
