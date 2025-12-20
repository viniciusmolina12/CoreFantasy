namespace CoreFantasy.Domain.User.ValueObjects
{
    public class IdentityId
    {
        public string Value { get; }

        private IdentityId(string Value)
        {
            this.Value = Value;
        }

        public static IdentityId Create(string Value)
        {
            return new IdentityId(Value);
        }
    }
}
