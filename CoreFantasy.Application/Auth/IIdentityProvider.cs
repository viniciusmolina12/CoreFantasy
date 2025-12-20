namespace CoreFantasy.Application.Auth
{
    public interface IIdentityProvider
    {
        Task<string> CreateUser(string email, string name);
    }

}
