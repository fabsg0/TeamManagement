using Api.TeamManagement.Models;
using Api.TeamManagement.Providers.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Api.TeamManagement.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class MemberController(IMemberProvider memberProvider, ILogger<MemberController> logger) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<MemberModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMembers(CancellationToken cancellationToken)
    {
        try
        {
            var members = await memberProvider.GetMembers(cancellationToken);
            return Ok(members);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to fetch members.");
            return NotFound();
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MemberModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMemberById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var member = await memberProvider.GetMemberById(id, cancellationToken);
            return Ok(member);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to fetch member.");
            return NotFound();
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateMember([FromBody] MemberModelDto member, CancellationToken cancellationToken)
    {
        try
        {
            await memberProvider.CreateMember(member.Member, member.DepartmentIds, cancellationToken);
            return CreatedAtAction(nameof(GetMemberById), new { id = member.Member.Id }, member);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create member.");
            return BadRequest();
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateMember([FromBody] MemberModelDto member, CancellationToken cancellationToken)
    {
        try
        {
            await memberProvider.UpdateMember(member.Member, member.DepartmentIds, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to update member.");
            return BadRequest();
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteMember(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await memberProvider.DeleteMember(id, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to delete member.");
            return BadRequest();
        }
    }
}