using SosyalApp2.Core.Models;

namespace SosyalApp2.Core.Interfaces
{
    public interface IFriendService
    {
        Task<IEnumerable<FriendRequest>> GetFriendRequestsAsync(int userId);
        Task<FriendRequest> SendFriendRequestAsync(int requesterId, int receiverId);
        Task<bool> AcceptFriendRequestAsync(int requestId);
        Task<bool> RejectFriendRequestAsync(int requestId);
        Task<bool> RemoveFriendAsync(int userId, int friendId);
        Task<IEnumerable<UserProfile>> SearchUsersAsync(string searchTerm);
        Task<bool> IsFriendAsync(int userId, int friendId);
    }
}