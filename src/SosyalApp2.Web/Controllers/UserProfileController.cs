using Microsoft.AspNetCore.Mvc;
using SosyalApp2.Core.Interfaces;
using SosyalApp2.Core.Models;

namespace SosyalApp2.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userService;
        private readonly IFriendService _friendService;

        public UserProfileController(IUserProfileService userService, IFriendService friendService)
        {
            _userService = userService;
            _friendService = friendService;
        }

        // // <summary>
        // // Gets the user profile for the authenticated user
        // // </summary>
        // // <param name="userId">The ID of the user</param>
        // // <returns>User profile information</returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<ActionResponse<UserProfile>>> GetUserProfile(int userId)
        {
            // In a real implementation, you would verify that the user is authenticated
            // and that they have permission to view this profile
            var profile = await _userService.GetUserProfileAsync(userId);

            if (profile == null)
            {
                return NotFound($"User profile not found for user ID: {userId}");
            }

            return Ok(profile);
        }

        // // <summary>
        // // Updates the user profile for the authenticated user
        // // </summary>
        // // <param name="userId">The ID of the user</param>
        // // <param name="profile">Updated profile information</param>
        // // <returns>Updated user profile</returns>
        [HttpPut("{userId}")]
        public async Task<ActionResult<ActionResponse<UserProfile>>> UpdateUserProfile(int userId, [FromBody] UserProfile profile)
        {
            // In a real implementation, you would verify that the user is authenticated
            // and that they are updating their own profile

            if (profile == null)
            {
                return BadRequest("Profile data is required");
            }

            if (profile.Id != userId)
            {
                return BadRequest("User ID in URL does not match user ID in request body");
            }

            try
            {
                var updatedProfile = await _userService.UpdateUserProfileAsync(profile);
                return Ok(updatedProfile);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // // <summary>
        // // Creates a new user profile
        // // </summary>
        // // <param name="profile">New profile information</param>
        // // <returns>Created user profile</returns>
        [HttpPost]
        public async Task<ActionResult<ActionResponse<UserProfile>>> CreateUserprofile([FromBody] UserProfile profile)
        {
            if (profile == null)
            {
                return BadRequest("Profile data is required");
            }

            try
            {
                var createdProfile = await _userService.CreateUserprofileAsync(profile);
                return CreatedAtAction(nameof(GetUserProfile), new { userId = createdProfile.Id }, createdProfile);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // // <summary>
        // // Deletes a user profile
        // // </summary>
        // // <param name="userId">The ID of the user</param>
        // // <returns>Success status</returns>
        [HttpDelete("{userId}")]
        public async Task<ActionResult<ActionResponse<bool>>> DeleteUserProfile(int userId)
        {
            // In a real implementation, you would verify that the user is authenticated
            // and that they have permission to delete this profile

            
            var result = await _userService.DeleteUserProfileAsync(userId);

            if (!result)
            {
                return NotFound($"User profile not found for user ID: {userId}");
            }

            return Ok(true);
        }

        // // <summary>
        // // Gets friend requests for a user
        // // </summary>
        // // <param name="userId">The ID of the user</param>
        // // <returns>List of friend requests</returns>
        [HttpGet("{userId}/friends")]
        public async Task<ActionResult<IEnumerable<FriendRequest>>> GetFriendRequests(int userId)
        {
            var friendRequests = await _friendService.GetFriendRequestsAsync(userId);
            return Ok(friendRequests);
        }

        // // <summary>
        // // Sends a friend request
        // // </summary>
        // // <param name="requesterId">The ID of the user sending the request</param>
        // // <param name="receiverId">The ID of the user receiving the request</param>
        // // <returns>Created friend request</returns>
        [HttpPost("{requesterId}/friends/{receiverId}")]
        public async Task<ActionResult<FriendRequest>> SendFriendRequest(int requesterId, int receiverId)
        {
            try
            {
                var friendRequest = await _friendService.SendFriendRequestAsync(requesterId, receiverId);
                return Ok(friendRequest);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // // <summary>
        // // Accepts a friend request
        // // </summary>
        // // <param name="requestId">The ID of the friend request</param>
        // // <returns>Success status</returns>
        [HttpPost("friends/{requestId}/accept")]
        public async Task<ActionResult<bool>> AcceptFriendRequest(int requestId)
        {
            var result = await _friendService.AcceptFriendRequestAsync(requestId);
            return Ok(result);
        }

        // // <summary>
        // // Rejects a friend request
        // // </summary>
        // // <param name="requestId">The ID of the friend request</param>
        // // <returns>Success status</returns>
        [HttpPost("friends/{requestId}/reject")]
        public async Task<ActionResult<bool>> RejectFriendRequest(int requestId)
        {
            var result = await _friendService.RejectFriendRequestAsync(requestId);
            return Ok(result);
        }

        // // <summary>
        // // Removes a friend
        // // </summary>
        // // <param name="userId">The ID of the user</param>
        // // <param name="friendId">The ID of the friend to remove</param>
        // // <returns>Success status</returns>
        [HttpDelete("{userId}/friends/{friendId}")]
        public async Task<ActionResult<bool>> RemoveFriend(int userId, int friendId)
        {
            var result = await _friendService.RemoveFriendAsync(userId, friendId);
            return Ok(result);
        }

        // // <summary>
        // // Searches for users by username or ID
        // // </summary>
        // // <param name="searchTerm">The search term</param>
        // // <returns>List of matching users</returns>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<UserProfile>>> SearchUsers(string searchTerm)
        {
            var users = await _friendService.SearchUsersAsync(searchTerm);
            return Ok(users);
        }
    }
}