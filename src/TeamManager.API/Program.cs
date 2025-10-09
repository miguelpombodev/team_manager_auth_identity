using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TeamManager.API.Extensions;
using TeamManager.Domain.Entities;
using TeamManager.Infrastructure.Extensions;
using TeamManager.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile(
        $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
        optional: true, reloadOnChange: true).AddEnvironmentVariables().AddCommandLine(args).Build();

builder.Host.ConfigureSerilog();

builder.Services
    .AddOpenApi()
    .AddInfrastructureServices(configuration)
    .AddIdentitySetup()
    .AddAuthenticationAndAuthorization();

var app = builder.Build();

app.AddSerilog();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

    if (!await roleManager.RoleExistsAsync(Roles.TeamLeader))
    {
        await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.TeamLeader));
    }

    if (!await roleManager.RoleExistsAsync(Roles.TeamMember))
    {
        await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.TeamMember));
    }
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.Run();