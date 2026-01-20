using SosyalApp2.Core.Models;

namespace SosyalApp2.Core.Interfaces
{
    public interface IUserStatisticsService
    {
        Task<UserProfile> CalculateUserStatisticsAsync(int userId);
    }
}