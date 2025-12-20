using CoreFantasy.Domain.Shared;

namespace CoreFantasy.Domain.User.ValueObjects
{
    public class Phone : ValueObject
    {
        public string Value { get; }
        private Phone(string Value)
        {
            this.Value = Value;
        }
        public static (Phone Phone, Notification Notification) Create(string Value)
        {
            string formattedValue = Value ?? string.Empty;
            var notification = Validate(formattedValue);
            var phone = notification.HasErrors() ? null : new Phone(formattedValue);
            return (phone, notification);
        }
        private static Notification Validate(string Value)
        {
            var notification = new Notification();
            var regex = @"^\+\d{1,3}\s?\(\d{1,3}\)\s?\d{4,5}-\d{4}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(Value, regex))
            {
                notification.AddError(typeof(Phone).Name, PhoneErrors.INVALID_PHONE);
            }
            return notification;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;

        }
    }

    public record PhoneErrors
    {
        public const string INVALID_PHONE = "Phone number is invalid.";
    }
}
