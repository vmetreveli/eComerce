using Catalog.Application.Abstractions.Messaging;
using Catalog.Application.Dto;

namespace Catalog.Application.Features.ProductFeatures.Commands;

public class UpdateProductCommand : ICommand<bool>
{
    public ProductDto ProductDto { get; set; }
}