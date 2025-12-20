using CoreFantasy.Domain.Shared;

namespace CoreFantasy.Domain.Player.ValueObjects.Agenda
{
    public sealed class ActionType : ValueObject
    {
        public static readonly ActionType Work = new("WORK");
        public static readonly ActionType Study = new("STUDY");
        public static readonly ActionType Sleep = new("SLEEP");

        public string Value { get; }

        private ActionType(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }

}
