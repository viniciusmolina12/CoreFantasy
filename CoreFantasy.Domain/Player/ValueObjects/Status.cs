using CoreFantasy.Domain.Shared;

namespace CoreFantasy.Domain.Player.ValueObjects
{
    public record StatusRules
    {
        public readonly static  int MAX_HEALTH = 100;
        public readonly static int MIN_HEALTH = 1;
    }
    public class Status : ValueObject
    {
        public int Health { get; }
         

        private Status(int Health)
        {
            this.Health = Health;
        }
        public static (Status Status, Notification Notification)Create(int Health)
        {
            Notification notification = Validate(Health);
            var health = notification.HasErrors() ? null : new Status(Health);
            return (health, notification);
        }

        private static Notification Validate(int Health)
        {
            Notification notification = new();
            if (Health < StatusRules.MIN_HEALTH)
            {
                notification.AddError(typeof(Status).Name, StatusErrors.HEALTH_CANNOT_BE_NEGATIVE);
            }else if (Health > StatusRules.MAX_HEALTH)
            {
                notification.AddError(typeof(Status).Name, StatusErrors.HEALTH_EXCEED_MAXIMUM);
            }
            return notification;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Health;
        }
    }

    public record StatusErrors
    {
        public const string HEALTH_CANNOT_BE_NEGATIVE = "Health cannot be negative.";
        public const string HEALTH_EXCEED_MAXIMUM = "Health cannot exceed maximum limit.";

    }
}
