using Api.TeamManagement.Models;

namespace Api.TeamManagement.Providers.Contracts;

public interface IMemberProvider
{
    Task<List<MemberModel>> GetMembers(CancellationToken cancellationToken);
    Task<MemberModel> GetMemberById(Guid id, CancellationToken cancellationToken);
    Task CreateMember(MemberModel member, CancellationToken cancellationToken);
    Task UpdateMember(MemberModel member, CancellationToken cancellationToken);
    Task DeleteMember(Guid id, CancellationToken cancellationToken);
}