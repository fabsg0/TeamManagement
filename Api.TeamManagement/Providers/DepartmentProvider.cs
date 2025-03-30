using Api.TeamManagement.Database;
using Api.TeamManagement.Entities;
using Api.TeamManagement.Providers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Api.TeamManagement.Providers;

public class DepartmentProvider(TeamManagementDbContext dbContext) : IDepartmentProvider
{
    public async Task<List<TbDepartment>> GetDepartments(CancellationToken cancellationToken)
    {
        var departments = await dbContext.TbDepartments
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return departments;
    }
    
    public async Task<TbDepartment> GetDepartmentById(Guid id, CancellationToken cancellationToken)
    {
        var department = await dbContext.TbDepartments
            .AsNoTracking()
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

        return department ?? throw new Exception("Department not found.");
    }
    
    public async Task CreateDepartment(string name, CancellationToken cancellationToken, string? icon = null)
    {
        var department = new TbDepartment
        {
            Name = name,
            Icon = icon
        };
        
        await dbContext.TbDepartments.AddAsync(department, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateDepartment(Guid id, string name, CancellationToken cancellationToken, string? icon = null)
    {
        var department = await dbContext.TbDepartments
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

        if (department is null) throw new Exception("Department not found.");
        
        department.Name = name;
        department.Icon = icon;
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteDepartment(Guid id, CancellationToken cancellationToken)
    {
        var department = await dbContext.TbDepartments
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

        if (department is null) throw new Exception("Department not found.");

        dbContext.Remove(department);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}