namespace CoreFantasy.Domain.User.Repositories
{
    public interface IUserRepository
    {
        public Task Create(User user);
    }
}
