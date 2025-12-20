using CoreFantasy.Application.Auth;

namespace CoreFantasy.Infrastructure.Auth
{
    public class KeycloakIdentityProvider : IIdentityProvider
    {
        public Task<string> CreateUser(string email, string name)
        {
            return Task.FromResult("");
        }
    }
}
