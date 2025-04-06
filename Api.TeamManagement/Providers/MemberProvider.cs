using Api.TeamManagement.Database;
using Api.TeamManagement.Entities;
using Api.TeamManagement.Models;
using Api.TeamManagement.Providers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Api.TeamManagement.Providers;

public class MemberProvider(TeamManagementDbContext dbContext, IPaymentProvider paymentProvider) : IMemberProvider
{
    public async Task<List<MemberModel>> GetMembers(CancellationToken cancellationToken)
    {
        var rawMembers = await dbContext.TbMembers
            .ToListAsync(cancellationToken);

        var departmentMembers = await dbContext.TbDepartmentMembers
            .Include(tbDepartmentMember => tbDepartmentMember.Department)
            .ToListAsync(cancellationToken);

        return rawMembers.Select(rawMember => new MemberModel
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
                Departments = departmentMembers.Where(x => x.MemberId == rawMember.Id)
                    .Select(x => new DepartmentInfoModel
                    {
                        Id = x.Department?.Id ?? Guid.Empty,
                        Name = x.Department?.Name,
                        Icon = x.Department?.Icon
                    })
                    .ToList(),
                MembershipPaid = paymentProvider.IsMembershipPaid(rawMember.Id, cancellationToken).Result
            })
            .ToList();
    }

    public async Task<MemberModel> GetMemberById(Guid id, CancellationToken cancellationToken)
    {
        var rawMember = await dbContext.TbMembers
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

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
                .Where(x => x.MemberId == rawMember.Id)
                .Select(x => new DepartmentInfoModel
                {
                    Id = x.Department?.Id ?? Guid.Empty,
                    Name = x.Department?.Name,
                    Icon = x.Department?.Icon
                })
                .ToList()
        };

        return member;
    }

    public async Task CreateMember(MemberModel member, List<Guid> departmentIds, CancellationToken cancellationToken)
    {
        var memberToCreate = new TbMember
        {
            Id = Guid.NewGuid(),
            FirstName = member.FirstName,
            LastName = member.LastName,
            Birthdate = member.Birthdate,
            Email = member.Email,
            Telephone = member.Telephone,
            Street = member.Street,
            Number = member.Number,
            ZipCode = member.ZipCode,
            City = member.City,
            Status = member.Status,
            MembershipFee = member.MembershipFee
        };

        await dbContext.TbMembers.AddAsync(memberToCreate, cancellationToken);

        foreach (var departmentMember in departmentIds.Select(departmentId => new TbDepartmentMember
                 {
                     Id = Guid.NewGuid(),
                     DepartmentId = departmentId,
                     MemberId = memberToCreate.Id
                 }))
        {
            await dbContext.TbDepartmentMembers.AddAsync(departmentMember, cancellationToken);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateMemberStatus(Guid id, CancellationToken cancellationToken)
    {
        var existingMember = await dbContext.TbMembers
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        if (existingMember is null) throw new Exception("Member not found.");

        existingMember.Status = existingMember.Status == "active" 
            ? "inactive" 
            :  "active";
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateMember(MemberModel member, List<Guid> departmentIds, CancellationToken cancellationToken)
    {
        var existingMember = await dbContext.TbMembers
            .SingleOrDefaultAsync(x => x.Id == member.Id, cancellationToken);

        if (existingMember is null) throw new Exception("Member not found.");

        existingMember.FirstName = member.FirstName;
        existingMember.LastName = member.LastName;
        existingMember.Birthdate = member.Birthdate;
        existingMember.Email = member.Email;
        existingMember.Telephone = member.Telephone;
        existingMember.Street = member.Street;
        existingMember.Number = member.Number;
        existingMember.ZipCode = member.ZipCode;
        existingMember.City = member.City;
        existingMember.Status = member.Status;
        existingMember.MembershipFee = member.MembershipFee;

        var existingLinks = await dbContext.TbDepartmentMembers
            .Where(x => x.MemberId == member.Id)
            .ToListAsync(cancellationToken);

        dbContext.TbDepartmentMembers.RemoveRange(existingLinks);

        foreach (var newLink in departmentIds.Select(departmentId => new TbDepartmentMember
                 {
                     Id = Guid.NewGuid(),
                     DepartmentId = departmentId,
                     MemberId = member.Id
                 }))
        {
            await dbContext.TbDepartmentMembers.AddAsync(newLink, cancellationToken);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }


    public async Task DeleteMember(Guid id, CancellationToken cancellationToken)
    {
        var member = await dbContext.TbMembers
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (member is null) throw new Exception("Member not found.");

        var departmentLinks = await dbContext.TbDepartmentMembers
            .Where(x => x.MemberId == id)
            .ToListAsync(cancellationToken);

        dbContext.TbDepartmentMembers.RemoveRange(departmentLinks);
        dbContext.TbMembers.Remove(member);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}