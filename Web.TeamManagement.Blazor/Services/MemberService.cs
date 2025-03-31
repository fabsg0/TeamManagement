using Api.TeamManagement.Models;
using Web.TeamManagement.Blazor.Services.Contracts;

namespace Web.TeamManagement.Blazor.Services;

public class MemberService(HttpClient httpClient) : IMemberService
{
    public async Task<List<MemberModel>> GetMembersAsync(CancellationToken cancellationToken = default)
    {
        var members =
            await httpClient.GetFromJsonAsync<List<MemberModel>>("http://localhost:5179/Member/GetMembers",
                cancellationToken);

        return members ?? [];
    }

    public async Task<MemberModel> GetMemberByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var member = await httpClient.GetFromJsonAsync<MemberModel>($"http://localhost:5179/Member/GetMemberById/{id}",
            cancellationToken);
        
        return member ?? throw new Exception("Member not found.");
    }

    public async Task CreateMemberAsync(MemberModelDto member, CancellationToken cancellationToken = default)
    {
        await httpClient.PostAsync("http://localhost:5179/Member/CreateMember", JsonContent.Create(member),
            cancellationToken);
    }

    public async Task UpdateMemberStatusAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await httpClient.PutAsync($"http://localhost:5179/Member/UpdateMemberStatus/{id}", null, cancellationToken);
    }

    public async Task UpdateMemberAsync(MemberModelDto member, CancellationToken cancellationToken = default)
    {
        await httpClient.PutAsync("http://localhost:5179/Member/UpdateMember", JsonContent.Create(member),
            cancellationToken);
    }

    public async Task DeleteMemberAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await httpClient.DeleteAsync($"http://localhost:5179/Member/DeleteMember/{id}", cancellationToken);
    }
}