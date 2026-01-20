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

        public UserProfileController(IUserProfileService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets the user profile for the authenticated user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>User profile information</returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserProfile>> GetUserProfile(int userId)
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

        /// <summary>
        /// Updates the user profile for the authenticated user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="profile">Updated profile information</param>
        /// <returns>Updated user profile</returns>
        [HttpPut("{userId}")]
        public async Task<ActionResult<UserProfile>> UpdateUserProfile(int userId, [FromBody] UserProfile profile)
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

        /// <summary>
        /// Creates a new user profile
        /// </summary>
        /// <param name="profile">New profile information</param>
        /// <returns>Created user profile</returns>
        [HttpPost]
        public async Task<ActionResult<UserProfile>> CreateUserprofile([FromBody] UserProfile profile)
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

        /// <summary>
        /// Deletes a user profile
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>Success status</returns>
        [HttpDelete("{userId}")]
        public async Task<ActionResult<bool>> DeleteUserProfile(int userId)
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
    }
}