using SosyalApp2.Core.Models;

namespace SosyalApp2.Core.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<UserProfile?> GetUserProfileAsync(int userId);
        Task<UserProfile> UpdateUserProfileAsync(UserProfile userProfile);
        Task<UserProfile> CreateUserprofileAsync(UserProfile userProfile);
        Task<bool> DeleteUserProfileAsync(int userId);
    }
}