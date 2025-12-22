using Bogus;
using CoreFantasy.Domain.Shared;
using Errors = CoreFantasy.Domain.Player.ValueObjects.AgeErrors;
using Rules = CoreFantasy.Domain.Player.ValueObjects.AgeRules;
using Sut = CoreFantasy.Domain.Player.ValueObjects.Age;

namespace CoreFantasy.Domain.Tests.Player
{
    public class Age
    {
        public Faker faker = new();
        [Fact]
        public void Should_Create_A_Valid_Age()
        {
            // Arrange
            int valid_age = faker.Random.Number(Rules.MIN_AGE, Rules.MAX_AGE);
            (Sut age, Notification notification) = Sut.Create(valid_age);
            Assert.False(notification.HasErrors());
            Assert.Equal(valid_age, age.Value);
        }

        [Fact]
        public void Should_Return_Error_If_Age_Is_Less_Than_Minimal()
        {
            // Arrange
            int invalid_age = Rules.MIN_AGE - 1;
            (Sut age, Notification notification) = Sut.Create(invalid_age);
            Assert.Null(age);
            Assert.True(notification.HasErrors());
            Assert.Contains(Errors.AGE_LESS_THAN_MINIMAL, notification.GetErrorsByContext("Age"));
        }

        [Fact]
        public void Should_Return_Error_If_Age_Is_More_Than_Max()
        {
            // Arrange
            int invalid_age = Rules.MAX_AGE + 1;
            (Sut age, Notification notification) = Sut.Create(invalid_age);
            Assert.Null(age);
            Assert.True(notification.HasErrors());
            Assert.Contains(Errors.AGE_EXCEED_MAXIMUM, notification.GetErrorsByContext("Age"));
        }

    }
}
