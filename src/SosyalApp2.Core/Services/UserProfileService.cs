using SosyalApp2.Core.Interfaces;
using SosyalApp2.Core.Models;

namespace SosyalApp2.Core.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userRepository;
        private readonly IUserStatisticsService _statisticsService;

        public UserProfileService(IUserProfileRepository userRepository, IUserStatisticsService statisticsService)
        {
            _userRepository = userRepository;
            _statisticsService = statisticsService;
        }

        public async Task<UserProfile?> GetUserProfileAsync(int userId)
        {
            return await _userRepository.GetUserProfileAsync(userId);
        }

        public async Task<UserProfile> UpdateUserProfileAsync(UserProfile userProfile)
        {
            // Validate the input
            if (userProfile == null)
                throw new ArgumentNullException(nameof(userProfile));

            // In a real implementation, you would perform validation here
            // For example: check if username is unique, validate email format, etc.

            var updatedProfile = await _userRepository.UpdateUserProfileAsync(userProfile);
            
            // Recalculate statistics after update
            var profileWithStats = await _statisticsService.CalculateUserStatisticsAsync(updatedProfile.Id);
            
            return profileWithStats;
        }

        public async Task<UserProfile> CreateUserprofileAsync(UserProfile userProfile)
        {
            if (userProfile == null)
                throw new ArgumentNullException(nameof(userProfile));

            // In a real implementation, you would perform validation here
            var createdProfile = await _userRepository.CreateUserprofileAsync(userProfile);
            
            // Calculate initial statistics
            var profileWithStats = await _statisticsService.CalculateUserStatisticsAsync(createdProfile.Id);
            
            return profileWithStats;
        }

        public async Task<bool> DeleteUserProfileAsync(int userId)
        {
            return await _userRepository.DeleteUserProfileAsync(userId);
        }
    }
}