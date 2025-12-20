using CoreFantasy.Application.Auth;
using CoreFantasy.Domain.Shared;
using CoreFantasy.Domain.User.Repositories;
using CoreFantasy.Domain.User.ValueObjects;
using UserEntity = CoreFantasy.Domain.User.User;
namespace CoreFantasy.Application.User.Usecases
{
    public class CreateUserCommand
    {
        public string Email { get; init; }
        public string Name { get; init; }
        public string Phone { get; init; }
    }
    public class CreateUserUsecase(IUserRepository userRepository, IIdentityProvider identityProvider)
    {
        
        public async Task Execute(CreateUserCommand command)
        {
            Notification notification = new();
            (Name name, Notification name_notification) = Name.Create(command.Name);
            notification.Append(name_notification);

            (Email email, Notification email_notification) = Email.Create(command.Email);
            notification.Append(email_notification);

            (Phone phone, Notification phone_notification) = Phone.Create(command.Phone);
            notification.Append(phone_notification);

            string identity_provider = await identityProvider.CreateUser(command.Name, command.Email);

            IdentityId identity_id = IdentityId.Create(identity_provider);
            var user = UserEntity.Create(
                identity_id,
                name,
                email,
                phone
            );
            await userRepository.Create(user);
        }
    }
}
