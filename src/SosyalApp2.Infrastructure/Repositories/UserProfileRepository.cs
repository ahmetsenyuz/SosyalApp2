using SosyalApp2.Core.Interfaces;
using SosyalApp2.Core.Models;

namespace SosyalApp2.Infrastructure.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        // In a real implementation, this would interact with a database
        // For now, we'll simulate with an in-memory collection
        
        private static readonly Dictionary<int, UserProfile> _profiles = new();
        private static int _nextId = 1;

        public async Task<UserProfile?> GetUserProfileAsync(int userId)
        {
            await Task.Delay(1); // Simulate async operation
            return _profiles.TryGetValue(userId, out var profile) ? profile : null;
        }

        public async Task<UserProfile> UpdateUserProfileAsync(UserProfile userProfile)
        {
            if (userProfile == null)
                throw new ArgumentNullException(nameof(userProfile));

            await Task.Delay(1); // Simulate async operation
            
            // In a real implementation, you would update the record in the database
            // For this simulation, we'll just update in memory
            if (_profiles.ContainsKey(userProfile.Id))
            {
                _profiles[userProfile.Id] = userProfile;
            }
            else
            {
                userProfile.Id = _nextId++;
                _profiles[userProfile.Id] = userProfile;
            }
            
            return userProfile;
        }

        public async Task<UserProfile> CreateUserprofileAsync(UserProfile userProfile)
        {
            if (userProfile == null)
                throw new ArgumentNullException(nameof(userProfile));

            await Task.Delay(1); // Simulate async operation
            
            // In a real implementation, you would insert a new record into the database
            // For this simulation, we'll just add to in memory collection
            userProfile.Id = _nextId++;
            _profiles[userProfile.Id] = userProfile;
            
            return userProfile;
        }

        public async Task<bool> DeleteUserProfileAsync(int userId)
        {
            await Task.Delay(1); // Simulate async operation
            return _profiles.Remove(userId);
        }
    }
}