using SosyalApp2.Core.Interfaces;
using SosyalApp2.Core.Models;

namespace SosyalApp2.Infrastructure.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        // In a real implementation, this would interact with a database
        // For now, we'll simulate with an in-memory collection
        
        private static readonly Dictionary<int, FriendRequest> _friendRequests = new();
        private static int _nextRequestId = 1;

        public async Task<FriendRequest?> GetFriendRequestAsync(int requesterId, int receiverId)
        {
            await Task.Delay(1); // Simulate async operation
            return _friendRequests.Values.FirstOrDefault(fr => fr.RequesterId == requesterId && fr.ReceiverId == receiverId);
        }

        public async Task<IEnumerable<FriendRequest>> GetFriendRequestsByUserIdAsync(int userId)
        {
            await Task.Delay(1); // Simulate async operation
            return _friendRequests.Values.Where(fr => fr.RequesterId == userId || fr.ReceiverId == userId);
        }

        public async Task<FriendRequest> CreateFriendRequestAsync(FriendRequest friendRequest)
        {
            await Task.Delay(1); // Simulate async operation

            // In a real implementation, you would insert a new record into the database
            // For this simulation, we'll just add to in-memory collection
            friendRequest.Id = _nextRequestId++;
            _friendRequests[friendRequest.Id] = friendRequest;
            
            return friendRequest;
        }

        public async Task<bool> AcceptFriendRequestAsync(int requestId)
        {
            await Task.Delay(1); // Simulate async operation
            
            if (_friendRequests.TryGetValue(requestId, out FriendRequest? request))
            {
                request.IsAccepted = true;
                request.AcceptedAt = DateTime.UtcNow;
                return true;
            }
            
            return false;
        }

        public async Task<bool> DeleteFriendRequestAsync(int requestId)
        {
            await Task.Delay(1); // Simulate async operation
            return _friendRequests.Remove(requestId);
        }

        public async Task<bool> IsFriendAsync(int userId, int friendId)
        {
            await Task.Delay(1); // Simulate async operation
            return _friendRequests.Values.Any(fr => 
                ((fr.RequesterId == userId && fr.ReceiverId == friendId) || 
                 (fr.RequesterId == friendId && fr.ReceiverId == userId)) && 
                fr.IsAccepted);
        }

        public async Task<IEnumerable<UserProfile>> SearchUsersAsync(string searchTerm)
        {
            await Task.Delay(1); // Simulate async operation
            // In a real implementation, this would query the database
            // For this simulation, we'll return an empty list
            return new List<UserProfile>();
        }
    }
}