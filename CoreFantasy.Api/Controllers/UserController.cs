using CoreFantasy.Application.User.Usecases;
using Microsoft.AspNetCore.Mvc;

namespace CoreFantasy.Api.Controllers
{
    public record CreateUserInput
    {
        public required string Email { get; init; }
        public required string Name { get; init; }
        public required string Phone { get; init; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserController(CreateUserUsecase createUserUsecase) : ControllerBase
    {
        private readonly CreateUserUsecase createUserUsecase = createUserUsecase;

        public async Task<bool> Get(CreateUserInput input)
        {
            var createUserCommand = new CreateUserCommand
            {
                Email = input.Email,
                Name = input.Name,
                Phone = input.Phone,
            };

            await this.createUserUsecase.Execute(createUserCommand);
            return true;
        }
    }
}
