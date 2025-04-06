namespace Api.TeamManagement.Models;

public class PaymentModel
{
    public Guid? MemberId { get; set; }
    public int? PaymentPeriod { get; set; }
    public int PaymentAmount { get; set; }
}