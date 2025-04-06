using Api.TeamManagement.Database;
using Api.TeamManagement.Providers;
using Api.TeamManagement.Providers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Api.TeamManagement;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<TeamManagementDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("TeamManagementDb")));

        builder.Services.AddScoped<IDepartmentProvider, DepartmentProvider>();
        builder.Services.AddScoped<IMemberProvider, MemberProvider>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}