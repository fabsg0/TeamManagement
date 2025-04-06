namespace Api.TeamManagement.Entities;

public partial class TbDepartment
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Icon { get; set; }

    public virtual ICollection<TbDepartmentMember> TbDepartmentMembers { get; set; } = new List<TbDepartmentMember>();
}