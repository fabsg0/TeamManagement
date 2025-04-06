using Api.TeamManagement.Models;

namespace Web.TeamManagement.Blazor.Services.Contracts;

public interface IPaymentService
{
    Task<List<PaymentModel>> GetPaymentsAsync(int year, CancellationToken cancellationToken = default);
    Task<bool> IsMembershipPaidAsync(Guid memberId, CancellationToken cancellationToken = default);
    Task PayMembership(PaymentModel payment, CancellationToken cancellationToken = default);
}