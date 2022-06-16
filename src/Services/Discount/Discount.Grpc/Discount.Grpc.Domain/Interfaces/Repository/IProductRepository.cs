using Discount.Grpc.Domain.Models.Entities;

namespace Discount.Grpc.Domain.Interfaces.Repository;

public interface IDiscountRepository
{
    Task<Coupon> GetDiscount(string productName, CancellationToken cancellationToken);

    Task<bool> CreateDiscount(Coupon coupon, CancellationToken cancellationToken);
    Task<bool> UpdateDiscount(Coupon coupon, CancellationToken cancellationToken);
    Task<bool> DeleteDiscount(string productName, CancellationToken cancellationToken);
}