using Api.TeamManagement.Providers.Contracts;
using BlazorDownloadFile;
using Microsoft.Extensions.Options;
using Serilog;
using Web.TeamManagement.Blazor.Components;
using Web.TeamManagement.Blazor.Models;
using Web.TeamManagement.Blazor.Services;
using Web.TeamManagement.Blazor.Services.Contracts;
using Web.TeamManagement.Blazor.ToastMessage;

namespace Web.TeamManagement.Blazor;

internal class Program
{
    public static void Main(string[] args)
    {
        // Set up Serilog for file logging
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("Logs/teamManagement.blazor-.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        var builder = WebApplication.CreateBuilder(args);

        // Use Serilog
        builder.Host.UseSerilog();

        builder.Services.AddScoped<ToastService>();

        builder.Services.Configure<ApiSettings>(
            builder.Configuration.GetSection("ApiSettings"));
        
        builder.Services.AddHttpClient<IDepartmentService, DepartmentService>((provider, client) =>
        {
            var settings = provider.GetRequiredService<IOptions<ApiSettings>>().Value;
            client.BaseAddress = new Uri(settings.BaseUrl);
        });
        builder.Services.AddHttpClient<IMemberService, MemberService>((provider, client) =>
        {
            var settings = provider.GetRequiredService<IOptions<ApiSettings>>().Value;
            client.BaseAddress = new Uri(settings.BaseUrl);
        });
        builder.Services.AddHttpClient<IPaymentService, PaymentService>((provider, client) =>
        {
            var settings = provider.GetRequiredService<IOptions<ApiSettings>>().Value;
            client.BaseAddress = new Uri(settings.BaseUrl);
        });
        builder.Services.AddBlazorDownloadFile();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

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
