namespace Api.TeamManagement.Entities;

public partial class TbMembershipFeePayment
{
    public Guid Id { get; set; }

    public Guid? MemberId { get; set; }

    public int PaymentAmount { get; set; }

    public int? PaymentPeriod { get; set; }

    public virtual TbMember? Member { get; set; }
}
