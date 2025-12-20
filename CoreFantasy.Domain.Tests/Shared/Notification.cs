using Sut = CoreFantasy.Domain.Shared.Notification;

namespace CoreFantasy.Domain.Tests.Shared
{
    public class Notification
    {
        [Fact]
        public void Should_Add_And_Retrieve_Errors_By_Context()
        {
            // Arrange
            Sut notification = new();
            string context = "User";
            string message1 = "Name is required.";
            string message2 = "Email is invalid.";
            // Act
            notification.AddError(context, message1);
            notification.AddError(context, message2);
            List<string> errors = notification.GetErrorsByContext(context);
            // Assert
            Assert.Contains(message1, errors);
            Assert.Contains(message2, errors);
            Assert.Equal(2, errors.Count);
        }

        [Fact]
        public void Should_Add_Other_Context()
        {
            // Arrange
            Sut notification = new();
            string context = "User";
            string message1 = "Name is required.";
            string message2 = "Email is invalid.";
            string context2 = "Phone";
            string message3 = "Phone number is required.";
            // Act
            notification.AddError(context, message1);
            notification.AddError(context, message2);
            notification.AddError(context2, message3);
            // Assert
            Assert.Contains(message1, notification.GetErrorsByContext(context));
            Assert.Contains(message2, notification.GetErrorsByContext(context));
            Assert.Contains(message3, notification.GetErrorsByContext(context2));
        }

        [Fact]
        public void Should_Return_Empty_If_Context_Does_Not_Exist()
        {
            // Arrange
            Sut notification = new();
            // Assert
            List<string> errors = notification.GetErrorsByContext("non_existent_context");
            Assert.Empty(errors);
       
        }
    }
}
