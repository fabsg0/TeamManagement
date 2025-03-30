using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Api.TeamManagement.Entities;
using Api.TeamManagement.Providers.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Api.TeamManagement.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DepartmentController(IDepartmentProvider departmentProvider, ILogger<DepartmentController> logger) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<TbDepartment>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDepartments(CancellationToken cancellationToken)
    {
        try
        {
            var departments = await departmentProvider.GetDepartments(cancellationToken);
            return Ok(departments);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to fetch departments.");
            return NotFound(ex);
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TbDepartment), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDepartmentById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var department = await departmentProvider.GetDepartmentById(id, cancellationToken);
            return Ok(department);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to fetch department.");
            return NotFound(ex);
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateDepartment([FromBody] string name, string? icon, CancellationToken cancellationToken)
    {
        try
        {
            await departmentProvider.CreateDepartment(name, cancellationToken, icon);
            return CreatedAtAction(nameof(GetDepartments), null);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create department.");
            return BadRequest(ex);
        }
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateDepartment(Guid id, [FromBody] string name,  string icon, CancellationToken cancellationToken)
    {
        try
        {
            await departmentProvider.UpdateDepartment(id, name, cancellationToken, icon);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to update department.");
            return BadRequest(ex);
        }
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteDepartment(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await departmentProvider.DeleteDepartment(id, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to delete department.");
            return BadRequest(ex);
        }
    }
}