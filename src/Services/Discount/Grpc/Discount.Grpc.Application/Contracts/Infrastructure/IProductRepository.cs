using System.Threading;
using System.Threading.Tasks;
using Discount.Grpc.Domain.Entities;

namespace Discount.Grpc.Application.Contracts.Infrastructure;

public interface IDiscountRepository
{
    Task<Coupon> GetDiscount(string productName, CancellationToken cancellationToken);

    Task<bool> CreateDiscount(Coupon coupon, CancellationToken cancellationToken);
    Task<bool> UpdateDiscount(Coupon coupon, CancellationToken cancellationToken);
    Task<bool> DeleteDiscount(string productName, CancellationToken cancellationToken);
}