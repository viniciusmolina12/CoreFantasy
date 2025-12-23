using CoreFantasy.Domain.Shared;
namespace CoreFantasy.Domain.Player.ValueObjects
{
    public record AgeRules
    {
        public readonly static int MAX_AGE = 85;
        public readonly static int MIN_AGE = 18;
    }
    public class Age(int Value) : ValueObject
    {
        public int Value { get; } = Value;
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
                notification.AddError(typeof(Age).Name, AgeErrors.AGE_LESS_THAN_MINIMAL);
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
        public static readonly string AGE_LESS_THAN_MINIMAL = $"Age cannot be less than {AgeRules.MIN_AGE}.";
        public static readonly string AGE_EXCEED_MAXIMUM = $"Age cannot be more than {AgeRules.MAX_AGE}.";
    }
}
