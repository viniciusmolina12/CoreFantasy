using CoreFantasy.Domain.Shared;

namespace CoreFantasy.Domain.User.ValueObjects
{
    public class Email : ValueObject
    {
        public string Value { get; }

        private Email(string Value)
        {
            this.Value = Value;
        }

        public static (Email Email, Notification Notification) Create(string Value)
        {
            var notification = Validate(Value);
            var email = notification.HasErrors() ? null : new Email(Value);
            return (email, notification);
        }

        private static Notification Validate(string Value)
        {
            var notification = new Notification();
            var regex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(Value, regex, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                notification.AddError(typeof(Email).Name, EmailErrors.INVALID_EMAIL);
            }
            return notification;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }

    public record EmailErrors
    {
        public const string INVALID_EMAIL = "Email is invalid.";
    }
}
