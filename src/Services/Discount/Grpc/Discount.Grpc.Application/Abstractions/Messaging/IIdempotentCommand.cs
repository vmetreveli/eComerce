using System;

namespace Discount.Grpc.Application.Abstractions.Messaging;

public interface IIdempotentCommand<out TResponse> : ICommand<TResponse>
{
    Guid RequestId { get; set; }
}