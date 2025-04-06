using Api.TeamManagement.Database;
using Api.TeamManagement.Providers;
using Api.TeamManagement.Providers.Contracts;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Api.TeamManagement;

internal class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("Logs/api.teamManagement-.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        var builder = WebApplication.CreateBuilder(args);

        // Use Serilog
        builder.Host.UseSerilog();

        // Add services to the container.
        builder.Services.AddDbContext<TeamManagementDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("TeamManagementDb")));

        builder.Services.AddScoped<IDepartmentProvider, DepartmentProvider>();
        builder.Services.AddScoped<IMemberProvider, MemberProvider>();
        builder.Services.AddScoped<IPaymentProvider, PaymentProvider>();

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
        
        
        // Minimal endpoint to serve logs
        app.MapGet("/.logs", async context =>
        {
            var logFile = Directory.GetFiles("Logs", "*.log")
                .OrderByDescending(f => f)
                .FirstOrDefault();

            if (logFile is null)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("No logs found.");
                return;
            }

            await using var stream = new FileStream(logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(stream);
            var content = await reader.ReadToEndAsync();

            var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("<!DOCTYPE html><html><head><meta charset='UTF-8'><style>");
            sb.AppendLine("body { font-family: monospace; background: #1e1e1e; color: white; padding: 1em; }");
            sb.AppendLine(".error { color: red; font-weight: bold; }");
            sb.AppendLine(".warning { color: orange; }");
            sb.AppendLine(".info { color: lightblue; }");
            sb.AppendLine(".debug { color: gray; }");
            sb.AppendLine("</style></head><body>");

            foreach (var line in lines)
            {
                var cssClass = line.Contains("ERROR") ? "error"
                    : line.Contains("WARN") ? "warning"
                    : line.Contains("INFO") ? "info"
                    : line.Contains("DEBUG") ? "debug"
                    : "";

                sb.AppendLine($"<div class='{cssClass}'>{System.Net.WebUtility.HtmlEncode(line)}</div>");
            }

            sb.AppendLine("</body></html>");

            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync(sb.ToString());
        });

        
        app.Run();
    }
}