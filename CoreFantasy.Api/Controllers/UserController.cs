using CoreFantasy.Application.User.Usecases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoreFantasy.Api.Controllers
{

    public record CreateUserInput
    {
        public string Email { get; init; }
        public string Name { get; init; }
        public string Phone { get; init; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CreateUserUsecase createUserUsecase;
        public UserController(CreateUserUsecase createUserUsecase)
        {
            this.createUserUsecase = createUserUsecase;
        }
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
