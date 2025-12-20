using CoreFantasy.Application.Auth;
using CoreFantasy.Application.User.Usecases;
using CoreFantasy.Domain.User.Repositories;
using CoreFantasy.Infrastructure.Auth;
using CoreFantasy.Infrastructure.Database;
using CoreFantasy.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddScoped<IIdentityProvider, KeycloakIdentityProvider>();
builder.Services.AddScoped<CreateUserUsecase>();

builder.Services.AddDbContext<CoreFantasyDbContext>(options =>
{
    options.UseSqlite("Data Source=corefantasy.db");
});
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
