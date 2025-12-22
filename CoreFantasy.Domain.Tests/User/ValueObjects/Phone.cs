using Bogus;
using CoreFantasy.Domain.Shared;
using Errors = CoreFantasy.Domain.User.ValueObjects.PhoneErrors;
using Sut = CoreFantasy.Domain.User.ValueObjects.Phone;
namespace CoreFantasy.Domain.Tests.User
{
    public class Phone
    {
        public Faker faker = new("pt_BR");
        [Fact]
        public void Should_Create_A_Valid_Phone()
        {
            // Arrange

            string valid_phone = $"+55 {faker.Phone.PhoneNumber("(##) 9####-####")}";

            (Sut phone, Notification notification) = Sut.Create(valid_phone);
            Assert.False(notification.HasErrors());
            Assert.Equal(valid_phone, phone.Value);
        }

        [Fact]
        public void Should_Return_Error_If_Phone_Is_Invalid()
        {
            // Arrange
            string invalid_phone = faker.Random.Number(8).ToString();
            (Sut email, Notification notification) = Sut.Create(invalid_phone);
            Assert.True(notification.HasErrors());
            Assert.Contains(Errors.INVALID_PHONE, notification.GetErrorsByContext("Phone"));
        }

    }
}
