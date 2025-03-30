namespace Api.TeamManagement.Entities;

public partial class TbDepartmentMember
{
    public Guid Id { get; set; }

    public Guid? MemberId { get; set; }

    public Guid? DepartmentId { get; set; }

    public virtual TbDepartment? Department { get; set; }

    public virtual TbMember? Member { get; set; }

    public virtual ICollection<TbMember> TbMembers { get; set; } = new List<TbMember>();
}
