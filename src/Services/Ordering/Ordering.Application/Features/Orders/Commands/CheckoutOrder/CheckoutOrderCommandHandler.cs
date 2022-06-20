using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;

    public CheckoutOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, IEmailService emailService,
        ILogger<CheckoutOrderCommandHandler> logger)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = _mapper.Map<Order>(request);
        var newOrder = await _orderRepository.AddAsync(orderEntity, cancellationToken);
        _logger.LogInformation($"Order {newOrder.Id} is successfully created.");
        await SendMail(newOrder);
        return newOrder.Id;
    }

    private async Task SendMail(Order order)
    {
        try
        {
            await _emailService.SendMail(new Email
            {
                To = order.EmailAddress,
                Subject = "Order was created",
                Body = $"Order {order.Id} is successfully created."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Order {order.Id} filed due to an error with mail service: {ex.Message}");
        }
    }
}