namespace Discount.Grpc.Application.Features.Discount.Queries.GetDiscount;

public class CouponVm
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
}