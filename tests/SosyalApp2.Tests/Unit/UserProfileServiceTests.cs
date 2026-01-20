using Moq;
using SosyalApp2.Core.Interfaces;
using SosyalApp2.Core.Models;
using SosyalApp2.Core.Services;

namespace SosyalApp2.Tests.Unit
{
    public class UserProfileServiceTests
    {
        private readonly Mock<IUserProfileRepository> _mockRepository;
        private readonly Mock<IUserStatisticsService> _mockStatisticsService;
        private readonly UserProfileService _service;

        public UserProfileServiceTests()
        {
            _mockRepository = new Mock<IUserProfileRepository>();
            _mockStatisticsService = new Mock<IUserStatisticsService>();
            _service = new UserProfileService(_mockRepository.Object, _mockStatisticsService.Object);
        }

        [Fact]
        public async Task GetUserProfileAsync_ReturnsProfile_WhenProfileExists()
        {
            // Arrange
            var userId = 1;
            var expectedProfile = new UserProfile
            {
                Id = userId,
                Username = "testuser",
                Bio = "Test user bio"
            };

            _mockRepository.Setup(repo => repo.GetUserProfileAsync(userId))
                .ReturnsAsync(expectedProfile);

            // Act
            var result = await _service.GetUserProfileAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProfile.Id, result.Id);
            Assert.Equal(expectedProfile.Username, result.Username);
        }

        [Fact]
        public async Task GetUserProfileAsync_ReturnsNull_WhenProfileDoesNotExist()
        {
            // Arrange
            var userId = 999;
            _mockRepository.Setup(repo => repo.GetUserProfileAsync(userId))
                .ReturnsAsync((UserProfile)null);

            // Act
            var result = await _service.GetUserProfileAsync(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUserProfileAsync_UpdatesAndReturnsProfile()
        {
            // Arrange
            var profileToUpdate = new UserProfile
            {
                Id = 1,
                Username = "updateduser",
                Bio = "Updated bio"
            };

            var updatedProfile = new UserProfile
            {
                Id = 1,
                Username = "updateduser",
                Bio = "Updated bio",
                TotalPoints = 100,
                CompletedTasksCount = 5
            };

            _mockRepository.Setup(repo => repo.UpdateUserProfileAsync(It.IsAny<UserProfile>()))
                .ReturnsAsync(profileToUpdate);
                
            _mockStatisticsService.Setup(service => service.CalculateUserStatisticsAsync(profileToUpdate.Id))
                .ReturnsAsync(updatedProfile);

            // Act
            var result = await _service.UpdateUserProfileAsync(profileToUpdate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedProfile.Id, result.Id);
            Assert.Equal(updatedProfile.TotalPoints, result.TotalPoints);
            Assert.Equal(updatedProfile.CompletedTasksCount, result.CompletedTasksCount);
        }

        [Fact]
        public async Task CreateUserprofileAsync_CreatesAndReturnsProfile()
        {
            // Arrange
            var newProfile = new UserProfile
            {
                Username = "newuser",
                Bio = "New user bio"
            };

            var createdProfile = new UserProfile
            {
                Id = 1,
                Username = "newuser",
                Bio = "New user bio",
                TotalPoints = 0,
                CompletedTasksCount = 0
            };

            _mockRepository.Setup(repo => repo.CreateUserprofileAsync(It.IsAny<UserProfile>()))
                .ReturnsAsync(createdProfile);
                
            _mockStatisticsService.Setup(service => service.CalculateUserStatisticsAsync(createdProfile.Id))
                .ReturnsAsync(createdProfile);

            // Act
            var result = await _service.CreateUserprofileAsync(newProfile);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createdProfile.Id, result.Id);
            Assert.Equal(createdProfile.Username, result.Username);
        }

        [Fact]
        public async Task DeleteUserProfileAsync_DeletesProfile()
        {
            // Arrange
            var userId = 1;
            _mockRepository.Setup(repo => repo.DeleteUserProfileAsync(userId))
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteUserProfileAsync(userId);

            // Assert
            Assert.True(result);
        }
    }
}