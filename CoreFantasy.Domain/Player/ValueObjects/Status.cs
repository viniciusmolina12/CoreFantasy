using CoreFantasy.Domain.Shared;

namespace CoreFantasy.Domain.Player.ValueObjects
{
    public record StatusRules
    {
        public readonly static  int MAX_HEALTH = 100;
        public readonly static int MIN_HEALTH = 0;
    }
    public sealed class Status : ValueObject
    {
        public int Health { get; }
        public decimal Money { get; init; }

        private Status(int Health, decimal money)
        {
            this.Health = Health;
            this.Money = money;
        }
        public static Status Create(int Health, decimal Money)
        {
            if (Health < 0) Health = 0;
            if (Health > StatusRules.MAX_HEALTH)
                Health = StatusRules.MAX_HEALTH;
            return new(Health, Money);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Health;
        }
    }
}
