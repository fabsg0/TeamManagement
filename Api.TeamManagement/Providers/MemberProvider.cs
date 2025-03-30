using Api.TeamManagement.Database;
using Api.TeamManagement.Models;
using Api.TeamManagement.Providers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Api.TeamManagement.Providers;

public class MemberProvider(TeamManagementDbContext dbContext) : IMemberProvider
{
    public async Task<List<MemberModel>> GetMembers(CancellationToken cancellationToken)
    {
        var rawMembers = await dbContext.TbMembers
            .ToListAsync(cancellationToken);

        var departmentMembers = await dbContext.TbDepartmentMembers
            .Include(tbDepartmentMember => tbDepartmentMember.Department)
            .ToListAsync(cancellationToken);

        var members = new List<MemberModel>();

        foreach (var rawMember in rawMembers)
        {
            members.Add(new MemberModel
            {
                Id = rawMember.Id,
                FirstName = rawMember.FirstName,
                LastName = rawMember.LastName,
                Birthdate = rawMember.Birthdate,
                Email = rawMember.Email,
                Telephone = rawMember.Telephone,
                Street = rawMember.Street,
                Number = rawMember.Number,
                ZipCode = rawMember.ZipCode,
                City = rawMember.City,
                Status = rawMember.Status,
                MembershipFee = rawMember.MembershipFee,
                Departments = departmentMembers
                    .Where(dm => dm.MemberId == rawMember.Id)
                    .Select(dm => (dm.Department?.Name, dm.Department?.Icon))
                    .ToList()
            });
        }

        return members;
    }

    public async Task<MemberModel> GetMemberById(Guid id, CancellationToken cancellationToken)
    {
        var rawMember = await dbContext.TbMembers
            .SingleOrDefaultAsync(member => member.Id == id, cancellationToken);

        if (rawMember is null) throw new Exception("Member not found.");

        var departmentMember = await dbContext.TbDepartmentMembers
            .Include(tbDepartmentMember => tbDepartmentMember.Department)
            .Where(tbDepartmentMember => tbDepartmentMember.MemberId == id)
            .ToListAsync(cancellationToken);

        var member = new MemberModel
        {
            Id = rawMember.Id,
            FirstName = rawMember.FirstName,
            LastName = rawMember.LastName,
            Birthdate = rawMember.Birthdate,
            Email = rawMember.Email,
            Telephone = rawMember.Telephone,
            Street = rawMember.Street,
            Number = rawMember.Number,
            ZipCode = rawMember.ZipCode,
            City = rawMember.City,
            Status = rawMember.Status,
            MembershipFee = rawMember.MembershipFee,
            Departments = departmentMember
                .Where(dm => dm.MemberId == rawMember.Id)
                .Select(dm => (dm.Department?.Name, dm.Department?.Icon))
                .ToList()
        };

        return member;
    }

    public async Task CreateMember(MemberModel member, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    public async Task UpdateMember(MemberModel member, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    public async Task DeleteMember(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}