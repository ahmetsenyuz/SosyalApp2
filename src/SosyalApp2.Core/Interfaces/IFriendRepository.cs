using SosyalApp2.Core.Models;

namespace SosyalApp2.Core.Interfaces
{
    public interface IFriendRepository
    {
        Task<FriendRequest?> GetFriendRequestAsync(int requesterId, int receiverId);
        Task<IEnumerable<FriendRequest>> GetFriendRequestsByUserIdAsync(int userId);
        Task<FriendRequest> CreateFriendRequestAsync(FriendRequest friendRequest);
        Task<bool> AcceptFriendRequestAsync(int requestId);
        Task<bool> DeleteFriendRequestAsync(int requestId);
        Task<bool> IsFriendAsync(int userId, int friendId);
        Task<IEnumerable<UserProfile>> SearchUsersAsync(string searchTerm);
    }
}