using System.Threading;
using System.Threading.Tasks;
using Discount.Domain.Models.Entities;

namespace Discount.Domain.Interfaces.Repository;

public interface IDiscountRepository
{
    Task<Coupon> GetDiscount(string productName, CancellationToken cancellationToken);

    Task<bool> CreateDiscount(Coupon coupon, CancellationToken cancellationToken);
    Task<bool> UpdateDiscount(Coupon coupon, CancellationToken cancellationToken);
    Task<bool> DeleteDiscount(string productName, CancellationToken cancellationToken);
}