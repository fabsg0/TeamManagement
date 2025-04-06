using Api.TeamManagement.Models;
using Web.TeamManagement.Blazor.Services.Contracts;

namespace Web.TeamManagement.Blazor.Services;

public class PaymentService(HttpClient httpClient) : IPaymentService
{
    public async Task<List<PaymentModel>> GetPaymentsAsync(int year, CancellationToken cancellationToken = default)
    {
        var payments =
            await httpClient.GetFromJsonAsync<List<PaymentModel>>(
                $"http://localhost:5179/Payment/GetPaymentsByYear/{year}", cancellationToken);
        return payments ?? [];
    }
    
    public async Task<bool> IsMembershipPaidAsync(Guid memberId, CancellationToken cancellationToken = default)
    {
        var isPaid = await httpClient.GetFromJsonAsync<bool>(
                $"http://localhost:5179/Payment/IsMembershipPaid/{memberId}", cancellationToken);
        return isPaid;
    }

    public async Task PayMembership(PaymentModel payment, CancellationToken cancellationToken = default)
    {
        await httpClient.PostAsync("http://localhost:5179/Payment/PayMembership",
            JsonContent.Create(payment), cancellationToken);
    }
}