using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SosyalApp2.Core.Interfaces;
using SosyalApp2.Core.Models;
using SosyalApp2.Infrastructure.Repositories;
using System.Net.Http.Json;

namespace SosyalApp2.Tests.Integration
{
    public class UserProfileIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UserProfileIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetUserProfile_ReturnsProfile_WhenProfileExists()
        {
            // Arrange
            var client = _factory.CreateClient();
            
            // Create a test user profile
            var testProfile = new UserProfile
            {
                Id = 1,
                Username = "testuser",
                Bio = "Test user bio",
                TotalPoints = 100,
                CompletedTasksCount = 5,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Set up the repository with our test data
            var serviceProvider = _factory.Services;
            var scope = serviceProvider.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IUserProfileRepository>();
            
            // Since we're using a mock repository, we'll directly use the implementation
            var userProfileRepo = new UserProfileRepository();
            await userProfileRepo.CreateUserprofileAsync(testProfile);

            // Act
            var response = await client.GetAsync("/api/userprofile/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var profile = await response.Content.ReadFromJsonAsync<UserProfile>();
            Assert.NotNull(profile);
            Assert.Equal("testuser", profile.Username);
            Assert.Equal(100, profile.TotalPoints);
        }

        [Fact]
        public async Task GetUserProfile_ReturnsNotFound_WhenProfileDoesNotExist()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/userprofile/999");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateUserProfile_UpdatesProfileSuccessfully()
        {
            // Arrange
            var client = _factory.CreateClient();
            
            // Create a test user profile
            var testProfile = new UserProfile
            {
                Id = 2,
                Username = "originaluser",
                Bio = "Original bio",
                TotalPoints = 50,
                CompletedTasksCount = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Set up the repository with our test data
            var serviceProvider = _factory.Services;
            var scope = serviceProvider.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IUserProfileRepository>();
            
            // Since we're using a mock repository, we'll directly use the implementation
            var userProfileRepo = new UserProfileRepository();
            await userProfileRepo.CreateUserprofileAsync(testProfile);

            // Update data
            var updatedProfile = new UserProfile
            {
                Id = 2,
                Username = "updateduser",
                Bio = "Updated bio",
                TotalPoints = 150,
                CompletedTasksCount = 8,
                CreatedAt = testProfile.CreatedAt,
                UpdatedAt = DateTime.UtcNow
            };

            // Act
            var response = await client.PutAsJsonAsync("/api/userprofile/2", updatedProfile);

            // Assert
            response.EnsureSuccessStatusCode();
            var profile = await response.Content.ReadFromJsonAsync<UserProfile>();
            Assert.NotNull(profile);
            Assert.Equal("updateduser", profile.Username);
            Assert.Equal(150, profile.TotalPoints);
        }

        [Fact]
        public async Task CreateUserprofile_CreatesProfileSuccessfully()
        {
            // Arrange
            var client = _factory.CreateClient();
            
            var newProfile = new UserProfile
            {
                Username = "newuser",
                Bio = "New user bio",
                TotalPoints = 0,
                CompletedTasksCount = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/userprofile", newProfile);

            // Assert
            response.EnsureSuccessStatusCode();
            var profile = await response.Content.ReadFromJsonAsync<UserProfile>();
            Assert.NotNull(profile);
            Assert.Equal("newuser", profile.Username);
            Assert.True(profile.Id > 0);
        }
    }
}