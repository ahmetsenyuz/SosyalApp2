using SosyalApp2.Core.Interfaces;
using SosyalApp2.Core.Models;

namespace SosyalApp2.Core.Services
{
    public class FriendService : IFriendService
    {
        private readonly IFriendRepository _friendRepository;

        public FriendService(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public async Task<IEnumerable<FriendRequest>> GetFriendRequestsAsync(int userId)
        {
            return await _friendRepository.GetFriendRequestsByUserIdAsync(userId);
        }

        public async Task<FriendRequest> SendFriendRequestAsync(int requesterId, int receiverId)
        {
            // Validate that users exist and are not the same
            if (requesterId == receiverId)
            {
                throw new ArgumentException("User cannot send friend request to themselves.");
            }

            // Check if a friend request already exists
            var existingRequest = await _friendRepository.GetFriendRequestAsync(requesterId, receiverId);
            if (existingRequest != null)
            {
                throw new InvalidOperationException("Friend request already exists.");
            }

            // Create new friend request
            var friendRequest = new FriendRequest
            {
                RequesterId = requesterId,
                ReceiverId = receiverId
            };

            return await _friendRepository.CreateFriendRequestAsync(friendRequest);
        }

        public async Task<bool> AcceptFriendRequestAsync(int requestId)
        {
            return await _friendRepository.AcceptFriendRequestAsync(requestId);
        }

        public async Task<bool> RejectFriendRequestAsync(int requestId)
        {
            // For rejecting, we simply delete the friend request
            return await _friendRepository.DeleteFriendRequestAsync(requestId);
        }

        public async Task<bool> RemoveFriendAsync(int userId, int friendId)
        {
            // In a real implementation, this would remove the friendship from the database
            // For now, we'll simulate by checking if they are friends
            return await _friendRepository.IsFriendAsync(userId, friendId);
        }

        public async Task<IEnumerable<UserProfile>> SearchUsersAsync(string searchTerm)
        {
            return await _friendRepository.SearchUsersAsync(searchTerm);
        }

        public async Task<bool> IsFriendAsync(int userId, int friendId)
        {
            return await _friendRepository.IsFriendAsync(userId, friendId);
        }
    }
}