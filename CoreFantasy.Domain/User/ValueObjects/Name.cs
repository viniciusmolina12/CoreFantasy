using CoreFantasy.Domain.Shared;

namespace CoreFantasy.Domain.User.ValueObjects
{
    public class Name : ValueObject
    {
        public string Value { get; }
        public static readonly int MIN_LENGTH = 4;
        public static readonly int MAX_LENGTH = 60;
        private Name(string Value)
        {
            this.Value = Value;
        }

        public static (Name Name, Notification Notification) Create(string Value)
        {
            var notification = Validate(Value);

            if (notification.HasErrors())
                return (null, notification);

            return (new Name(Value), notification);
        }

        private static Notification Validate(string Value)
        {
            var notification = new Notification();
            if (Value.Length < MIN_LENGTH)
            {
                notification.AddError(typeof(Name).Name, NameErrors.NAME_LESS_MIN_LENGTH);
            }
            else if (Value.Length > MAX_LENGTH)
            {
                notification.AddError(typeof(Name).Name, NameErrors.NAME_EXCEED_MAX_LENGTH);
            }
            return notification;
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }


    public record NameErrors 
    {
        public const string NAME_LESS_MIN_LENGTH = "Name cannot be less than 4 characters.";
        public const string NAME_EXCEED_MAX_LENGTH = "Name cannot exceed 100 characters.";
    }
}
