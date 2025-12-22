using Bogus;
using CoreFantasy.Domain.Shared;
using Errors = CoreFantasy.Domain.User.ValueObjects.EmailErrors;
using Sut = CoreFantasy.Domain.User.ValueObjects.Email;
namespace CoreFantasy.Domain.Tests.User
{
    public class Email
    {
        public Faker faker = new();
        [Fact]
        public void Should_Create_A_Valid_Email()
        {
            // Arrange
            string valid_email = faker.Internet.Email();
            (Sut email, Notification notification) = Sut.Create(valid_email);
            Assert.False(notification.HasErrors());
            Assert.Equal(valid_email, email.Value);
        }

        [Fact]
        public void Should_Return_Error_If_Email_Is_Invalid()
        {
            // Arrange
            string invalid_email = faker.Random.Word();
            (Sut email, Notification notification) = Sut.Create(invalid_email);
            Assert.True(notification.HasErrors());
            Assert.Contains(Errors.INVALID_EMAIL, notification.GetErrorsByContext("Email"));
        }

    }
}
