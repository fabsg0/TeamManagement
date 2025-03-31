using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Api.TeamManagement.Entities;
using Api.TeamManagement.Models;
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
            return NotFound();
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
            return NotFound();
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto department, CancellationToken cancellationToken)
    {
        try
        {
            await departmentProvider.CreateDepartment(department.Name, cancellationToken, department.Icon);
            return CreatedAtAction(nameof(GetDepartments), null);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create department.");
            return BadRequest();
        }
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateDepartment(Guid id, [FromBody] DepartmentDto department, CancellationToken cancellationToken)
    {
        try
        {
            await departmentProvider.UpdateDepartment(id,department.Name, cancellationToken, department.Icon);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to update department.");
            return BadRequest();
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
            return BadRequest();
        }
    }
}