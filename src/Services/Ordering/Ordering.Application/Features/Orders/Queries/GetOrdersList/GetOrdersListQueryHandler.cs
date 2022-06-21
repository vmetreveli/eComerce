using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery,List<OrderVm>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<List<OrderVm>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
       var orderList= await _orderRepository.GetOrdersByUserName(request.UserName,cancellationToken);
      return _mapper.Map<List<OrderVm>>(orderList);

    }
}