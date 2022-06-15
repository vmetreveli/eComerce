using Catalog.Application.Abstractions.Messaging;

namespace Catalog.Application.Features.ProductFeatures.Commands;

public class DeleteProductCommand : ICommand<bool>
{
    public string Id { get; set; }
}