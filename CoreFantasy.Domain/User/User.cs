using CoreFantasy.Domain.User.ValueObjects;

namespace CoreFantasy.Domain.User
{
    public class UserId
    {
        public Guid Value { get; }
        private UserId(Guid Value)
        {
            this.Value = Value;
        }
        public static UserId Create(Guid? Value = null)
        {
            return new UserId(Value ?? Guid.NewGuid());
        }
    }
    public class User
    {
        public UserId Id { get; }
        public IdentityId IdentityId { get; }
        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Phone Phone { get; private set; }

        private User(
            IdentityId identityId,
            UserId id,
            Name name,
            Email email,
            Phone phone)
        {
            IdentityId = identityId;
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
        }

        public static User Create(
            IdentityId identityId,
            Name name,
            Email email,
            Phone phone)
        {
            return new User(
                identityId,
                UserId.Create(),
                name,
                email,
                phone
            );
        }


        public void ChangeName(Name name)
        {
            this.Name = name;
        }

        public void ChangeEmail(Email email)
        {
            this.Email = email;
        }

        public void ChangePhone(Phone phone)
        {
            this.Phone = phone;
        }

    }

}
