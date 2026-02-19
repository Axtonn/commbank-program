using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using CommBank.Controllers;
using CommBank.Models;
using CommBank.Tests.Fakes;


public class GoalControllerTests
{
    [Fact]
    public async Task GetForUser_ReturnsOnlyGoalsForThatUser()
    {
        // Arrange
        var userId = "62a3f5e0102e921da1253d33";

        var goals = new List<Goal>
        {
            new Goal { Id = "62a3f5e0102e921da1253d32", UserId = userId, Name = "G1" },
            new Goal { Id = "62a3f5e0102e921da1253d34", UserId = userId, Name = "G2" },
            new Goal { Id = "62a3f5e0102e921da1253d99", UserId = "62a3f5e0102e921da1253d00", Name = "Other user" },
        };

        var goalsService = new FakeGoalsService(goals);
        var controller = new GoalController(goalsService);

        // Act
        var result = await controller.GetForUser(userId);

        // Assert
        Assert.NotNull(result);

        foreach (var goal in result)
        {
            Assert.IsAssignableFrom<Goal>(goal);
            Assert.Equal(userId, goal.UserId);
        }
    }
}
