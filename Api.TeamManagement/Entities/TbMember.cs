﻿namespace Api.TeamManagement.Entities;

public partial class TbMember
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly? Birthdate { get; set; }

    public string? Email { get; set; }

    public string? Telephone { get; set; }

    public string? Street { get; set; }

    public string? Number { get; set; }

    public int? ZipCode { get; set; }

    public string? City { get; set; }

    public string Status { get; set; } = null!;

    public int MembershipFee { get; set; }

    public virtual ICollection<TbDepartmentMember> TbDepartmentMembers { get; set; } = new List<TbDepartmentMember>();
}
