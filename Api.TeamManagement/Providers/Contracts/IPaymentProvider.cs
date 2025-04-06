using Api.TeamManagement.Entities;
using Api.TeamManagement.Models;

namespace Api.TeamManagement.Providers.Contracts;

public interface IPaymentProvider
{
    Task<List<PaymentModel>> GetPaymentsByYear(int year, CancellationToken cancellationToken);
    Task<bool> IsMembershipPaid(Guid memberId, CancellationToken cancellationToken);
    Task PayMembership(PaymentModel payment, CancellationToken cancellationToken);
}