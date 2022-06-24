using System.Threading;
using System.Threading.Tasks;
using Basket.API.Entities;

namespace Basket.Application.Contracts.Persistence;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken);
    Task<ShoppingCart> UpdateBasket(ShoppingCart basket, CancellationToken cancellationToken);
    Task DeleteBasket(string userName, CancellationToken cancellationToken);
}