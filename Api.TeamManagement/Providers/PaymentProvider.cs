using Api.TeamManagement.Database;
using Api.TeamManagement.Entities;
using Api.TeamManagement.Models;
using Api.TeamManagement.Providers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Api.TeamManagement.Providers;

public class PaymentProvider(TeamManagementDbContext dbContext) : IPaymentProvider
{
    public async Task<List<PaymentModel>> GetPaymentsByYear(int year, CancellationToken cancellationToken)
    {
        var payments = await dbContext.TbMembershipFeePayments
            .AsNoTracking()
            .Where(x => x.PaymentPeriod == year)
            .Select(x => new PaymentModel
            {
                MemberId = x.MemberId,
                PaymentPeriod = x.PaymentPeriod,
                PaymentAmount = x.PaymentAmount
            })
            .ToListAsync(cancellationToken);

        return payments;
    }

    public async Task<bool> IsMembershipPaid(Guid memberId, CancellationToken cancellationToken)
    {
        var member = await dbContext.TbMembers
            .SingleOrDefaultAsync(x => x.Id == memberId, cancellationToken);
        if (member is null) throw new Exception("Member not found.");
        
        var payment = await dbContext.TbMembershipFeePayments
            .AsNoTracking()
            .Where(x => x.MemberId == memberId && x.PaymentPeriod == DateTime.Now.Year)
            .SingleOrDefaultAsync(cancellationToken);

        return payment != null;
    }

    public async Task PayMembership(PaymentModel payment, CancellationToken cancellationToken)
    {
        var member = await dbContext.TbMembers
            .SingleOrDefaultAsync(x => x.Id == payment.MemberId, cancellationToken);
        if (member is null) throw new Exception("Member not found.");
        
        var newPayment = new TbMembershipFeePayment
        {
            MemberId = payment.MemberId,
            PaymentPeriod = payment.PaymentPeriod,
            PaymentAmount = payment.PaymentAmount
        };
        
        await dbContext.TbMembershipFeePayments.AddAsync(newPayment, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}