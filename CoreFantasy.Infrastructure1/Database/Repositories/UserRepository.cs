using CoreFantasy.Domain.User.Repositories;

namespace CoreFantasy.Infrastructure.Database.Repositories
{
    public class UserRepository(CoreFantasyDbContext context) : IUserRepository
    {
        public async Task Create(Domain.User.User user)
        {
            await context.AddAsync<Domain.User.User>(user);
            await context.SaveChangesAsync();
        }
    }
}
