using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamManager.Domain.Entities;
using TeamManager.Infrastructure.Extensions;
using TeamManager.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services
    .AddInfrastructureServices()
    .AddIdentitySetup();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync(Roles.TeamLeader))
    {
        await roleManager.CreateAsync(new IdentityRole(Roles.TeamLeader));
    }

    if (!await roleManager.RoleExistsAsync(Roles.TeamMember))
    {
        await roleManager.CreateAsync(new IdentityRole(Roles.TeamMember));
    }
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.Run();