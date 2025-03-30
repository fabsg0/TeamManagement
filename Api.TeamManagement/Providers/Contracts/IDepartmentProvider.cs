using Api.TeamManagement.Entities;

namespace Api.TeamManagement.Providers.Contracts;

public interface IDepartmentProvider
{
    Task<List<TbDepartment>> GetDepartments(CancellationToken cancellationToken);
    Task<TbDepartment> GetDepartmentById(Guid id, CancellationToken cancellationToken);
    Task CreateDepartment(string name, CancellationToken cancellationToken, string? icon = null);
    Task UpdateDepartment(Guid id, string name, CancellationToken cancellationToken, string? icon = null);
    Task DeleteDepartment(Guid id, CancellationToken cancellationToken);
}