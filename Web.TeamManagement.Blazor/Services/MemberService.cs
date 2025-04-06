using Api.TeamManagement.Models;
using Web.TeamManagement.Blazor.Services.Contracts;

namespace Web.TeamManagement.Blazor.Services;

public class MemberService(HttpClient httpClient) : IMemberService
{
    public async Task<List<MemberModel>> GetMembersAsync(CancellationToken cancellationToken = default)
    {
        var members =
            await httpClient.GetFromJsonAsync<List<MemberModel>>("/Member/GetMembers",
                cancellationToken);

        return members ?? [];
    }

    public async Task<MemberModel> GetMemberByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var member = await httpClient.GetFromJsonAsync<MemberModel>($"/Member/GetMemberById/{id}",
            cancellationToken);
        
        return member ?? throw new Exception("Member not found.");
    }

    public async Task<HttpResponseMessage> CreateMemberAsync(MemberModelDto member, CancellationToken cancellationToken = default)
    {
        member.Member.Status = "active";
        member.Member.MembershipFee = 0;
        
        return await httpClient.PostAsync("/Member/CreateMember", JsonContent.Create(member),
            cancellationToken);
    }

    public async Task UpdateMemberStatusAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await httpClient.PutAsync($"/Member/UpdateMemberStatus/{id}", null, cancellationToken);
    }

    public async Task UpdateMemberAsync(MemberModelDto member, CancellationToken cancellationToken = default)
    {
        await httpClient.PutAsync("/Member/UpdateMember", JsonContent.Create(member),
            cancellationToken);
    }

    public async Task DeleteMemberAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await httpClient.DeleteAsync($"/Member/DeleteMember/{id}", cancellationToken);
    }
}