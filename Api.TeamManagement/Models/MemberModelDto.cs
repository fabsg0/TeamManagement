namespace Api.TeamManagement.Models;

public class MemberModelDto
{
    public MemberModel Member { get; set; }
    public List<Guid> DepartmentIds { get; set; }
}