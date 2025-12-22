using Bogus;
using CoreFantasy.Domain.Shared;
using Errors = CoreFantasy.Domain.User.ValueObjects.NameErrors;
using Sut = CoreFantasy.Domain.User.ValueObjects.Name;
namespace CoreFantasy.Domain.Tests.User
{
    public class Name
    {
        public Faker faker = new();
        [Fact]
        public void Should_Create_A_Valid_Name()
        {
            // Arrange
            var valid_name = this.faker.Name.FullName();
            (Sut name, Notification notification) = Sut.Create(valid_name);
            Assert.False(notification.HasErrors());
            Assert.Equal(valid_name, name.Value);
        }

        [Fact]
        public void Should_Return_Error_If_Name_Less_Min_Length()
        {
            // Arrange
            var invalid_name = this.faker.Random.String(Sut.MIN_LENGTH - 1);
            (Sut name, Notification notification) = Sut.Create(invalid_name);
            Assert.Null(name);
            Assert.True(notification.HasErrors());
            Assert.Contains(Errors.NAME_LESS_MIN_LENGTH, notification.GetErrorsByContext("Name"));
        }

        [Fact]
        public void Should_Return_Error_If_Name_Exceed_Max_Length()
        {
            // Arrange
            var invalid_name = this.faker.Random.String(Sut.MAX_LENGTH + 1);
            (Sut name, Notification notification) = Sut.Create(invalid_name);
            Assert.Null(name);
            Assert.True(notification.HasErrors());
            Assert.Contains(Errors.NAME_EXCEED_MAX_LENGTH, notification.GetErrorsByContext("Name"));
        }
    }
}
