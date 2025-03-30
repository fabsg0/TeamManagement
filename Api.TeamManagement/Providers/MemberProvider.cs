using Api.TeamManagement.Database;
using Api.TeamManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.TeamManagement.Providers;

public class MemberProvider(TeamManagementDbContext dbContext)
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
}