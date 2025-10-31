using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamManager.API.Endpoints;
using TeamManager.API.Extensions;
using TeamManager.Application.Extensions;
using TeamManager.Domain.Entities;
using TeamManager.Infrastructure.Extensions;
using TeamManager.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile(
        $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
        optional: true,
        reloadOnChange: true
    )
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

builder.Host.ConfigureSerilog();


builder.Services
    .AddOpenApi()
    .AddInfrastructureServices(configuration)
    .AddIdentitySetup()
    .AddAuthorizationServices()
    .AddApiServices()
    .AddUseCases()
    .AddValidators()
    .AddHealthChecksServices(configuration)
    .AddSwaggerGen();

var app = builder.Build();

app.AddSerilog();

if (app.Environment.IsDevelopment())
{
    app.AddSwagger();

    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    
    if (!await roleManager.RoleExistsAsync(Roles.SystemAdmin))
    {
        await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.SystemAdmin));
    }

    if (!await roleManager.RoleExistsAsync(Roles.TeamLeader))
    {
        await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.TeamLeader));
    }

    if (!await roleManager.RoleExistsAsync(Roles.TeamMember))
    {
        await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.TeamMember));
    }
}

app.UseRouting();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.MapHealthChecks("/api/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI(x =>
    {
        x.UIPath = "/health-dashboard";
    }
);

await app.SeedDatabaseAsync();

AuthEndpoints.MapEndpoints(app);
TeamsEndpoints.MapEndpoints(app);

await app.RunAsync();