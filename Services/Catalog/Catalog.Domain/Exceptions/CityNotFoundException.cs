namespace Catalog.Domain.Exceptions;

public sealed class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(int productId)
        : base($"The product with the identifier {productId} was not found.")
    {
    }

    public ProductNotFoundException(string productName)
        : base($"The product with the Name {productName} was not found.")
    {
    }
}