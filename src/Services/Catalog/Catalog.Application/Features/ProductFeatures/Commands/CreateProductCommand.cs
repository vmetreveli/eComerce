using Catalog.Application.Abstractions.Messaging;
using Catalog.Application.Dto;
using MediatR;

namespace Catalog.Application.Features.ProductFeatures.Commands;

public class CreateProductCommand : ICommand<Unit>
{
    public ProductDto ProductDto { get; set; }
}