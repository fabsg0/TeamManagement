using Api.TeamManagement.Database;
using Api.TeamManagement.Providers;
using Api.TeamManagement.Providers.Contracts;

namespace Api.TeamManagement;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<TeamManagementDbContext>();

        builder.Services.AddScoped<IDepartmentProvider, DepartmentProvider>();
        builder.Services.AddScoped<IMemberProvider, MemberProvider>();
        
        // Add services to the container.

        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
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