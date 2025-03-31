using Api.TeamManagement.Entities;
using Api.TeamManagement.Models;
using Web.TeamManagement.Blazor.Services.Contracts;

namespace Web.TeamManagement.Blazor.Services;

public class DepartmentService(HttpClient httpClient) : IDepartmentService
{
    public async Task<List<TbDepartment>> GetDepartmentsAsync(CancellationToken cancellationToken = default)
    {
        var departments =
            await httpClient.GetFromJsonAsync<List<TbDepartment>>("http://localhost:5179/Department/GetDepartments",
                cancellationToken);
        return departments ?? [];
    }

    public async Task<TbDepartment> GetDepartmentByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var department =
            await httpClient.GetFromJsonAsync<TbDepartment>($"http://localhost:5179/Department/GetDepartmentById/{id}",
                cancellationToken);

        return department ?? throw new Exception("Department not found.");
    }

    public async Task CreateDepartmentAsync(DepartmentDto department, CancellationToken cancellationToken = default)
    {
        await httpClient.PostAsync("http://localhost:5179/Department/CreateDepartment", JsonContent.Create(department),
            cancellationToken);
    }

    public async Task UpdateDepartmentAsync(Guid id, DepartmentDto department,
        CancellationToken cancellationToken = default)
    {
        await httpClient.PutAsync($"http://localhost:5179/Department/UpdateDepartment/{id}",
            JsonContent.Create(department),
            cancellationToken);
    }

    public async Task DeleteDepartmentAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await httpClient.DeleteAsync($"http://localhost:5179/Department/DeleteDepartment/{id}", cancellationToken);
    }
}