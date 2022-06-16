namespace Discount.Grpc.Domain.Exceptions;

public sealed class DiscountNotFoundException : NotFoundException
{
    public DiscountNotFoundException(int productId)
        : base($"The Discount with the identifier {productId} was not found.")
    {
    }

    public DiscountNotFoundException(string productName)
        : base($"The Discount on the Product {productName} was not found.")
    {
    }
}