using BlazorDownloadFile;
using Web.TeamManagement.Blazor.Components;
using Web.TeamManagement.Blazor.Services;
using Web.TeamManagement.Blazor.Services.Contracts;
using Web.TeamManagement.Blazor.ToastMessage;

namespace Web.TeamManagement.Blazor;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<ToastService>();
        builder.Services.AddHttpClient<IDepartmentService, DepartmentService>();
        builder.Services.AddHttpClient<IMemberService, MemberService>();
        builder.Services.AddBlazorDownloadFile();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();


        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}