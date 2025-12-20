using CoreFantasy.Domain.Shared;
//TODO - TEST
namespace CoreFantasy.Domain.Player.ValueObjects
{
    file record AgeRules
    {
        public static int MAX_AGE = 85;
        public static int MIN_AGE = 18;
    }
    public class Age(int Value) : ValueObject
    {
        private int Value { get; } = Value;
        public static (Age Age, Notification Notification) Create(int Value)
        {
            Notification notification = Validate(Value);
            var age = notification.HasErrors() ? null : new Age(Value);
            return (age, notification);
        }

        private static Notification Validate(int Value)
        {
            Notification notification = new();
            if (Value < AgeRules.MIN_AGE)
            {
                notification.AddError(typeof(Age).Name, AgeErrors.AGE_CANNOT_BE_NEGATIVE);
            }else if (Value > AgeRules.MAX_AGE)
            {
                notification.AddError(typeof(Age).Name, AgeErrors.AGE_EXCEED_MAXIMUM);
            }
            return notification;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }

    public record AgeErrors
    {
        public static readonly string AGE_CANNOT_BE_NEGATIVE = $"Age cannot be less than {AgeRules.MIN_AGE}.";
        public static readonly string AGE_EXCEED_MAXIMUM = $"Age cannot be more than {AgeRules.MAX_AGE}.";
    }
}
