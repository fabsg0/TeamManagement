using Api.TeamManagement.Entities;
using Api.TeamManagement.Models;

namespace Web.TeamManagement.Blazor.Services.Contracts;

public interface IDepartmentService
{
    Task<List<TbDepartment>> GetDepartmentsAsync(CancellationToken cancellationToken = default);
    Task<TbDepartment> GetDepartmentByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task CreateDepartmentAsync(DepartmentDto department, CancellationToken cancellationToken = default);

    Task UpdateDepartmentAsync(Guid id, DepartmentDto department,
        CancellationToken cancellationToken = default);

    Task DeleteDepartmentAsync(Guid id, CancellationToken cancellationToken = default);
}