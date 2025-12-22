using Sut = CoreFantasy.Domain.Player.ValueObjects.Status;
using Errors = CoreFantasy.Domain.Player.ValueObjects.StatusErrors;
using Rules = CoreFantasy.Domain.Player.ValueObjects.StatusRules;
using Bogus;
using CoreFantasy.Domain.Shared;

namespace CoreFantasy.Domain.Tests.Player
{
    public class Status
    {
        public Faker faker = new();
        [Fact]
        public void Should_Create_A_Valid_Status()
        {
            // Arrange
            int health = faker.Random.Number(Rules.MIN_HEALTH, Rules.MAX_HEALTH);
            (Sut status, Notification notification) = Sut.Create(health);
            Assert.False(notification.HasErrors());
            Assert.Equal(health, status.Health);
        }

        [Fact]
        public void Should_Return_Error_If_Health_Is_Less_Than_Minimal()
        {
            // Arrange
            int invalid_health = Rules.MIN_HEALTH - 1;
            (Sut status, Notification notification) = Sut.Create(invalid_health);
            Assert.Null(status);
            Assert.True(notification.HasErrors());
            Assert.Contains(Errors.HEALTH_CANNOT_BE_NEGATIVE, notification.GetErrorsByContext("Status"));
        }

        [Fact]
        public void Should_Return_Error_If_Health_Is_More_Than_Max()
        {
            // Arrange
            int invalid_health = Rules.MAX_HEALTH + 1;
            (Sut status, Notification notification) = Sut.Create(invalid_health);
            Assert.Null(status);
            Assert.True(notification.HasErrors());
            Assert.Contains(Errors.HEALTH_EXCEED_MAXIMUM, notification.GetErrorsByContext("Status"));
        }

    }
}
