using Api.TeamManagement.Models;

namespace Web.TeamManagement.Blazor.Services.Contracts;

public interface IMemberService
{
    Task<List<MemberModel>> GetMembersAsync(CancellationToken cancellationToken = default);
    Task<MemberModel> GetMemberByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<HttpResponseMessage> CreateMemberAsync(MemberModelDto member, CancellationToken cancellationToken = default);
    Task UpdateMemberStatusAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateMemberAsync(MemberModelDto member, CancellationToken cancellationToken = default);
    Task DeleteMemberAsync(Guid id, CancellationToken cancellationToken = default);
}