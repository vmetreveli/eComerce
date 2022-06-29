using System.Threading.Tasks;
using AutoMapper;
using Discount.Grpc.Application.Features.Discount.Commands.DeleteDiscount;
using Discount.Grpc.Application.Features.Discount.Commands.UpdateDiscount;
using Discount.Grpc.Application.Features.Discount.Queries.GetDiscount;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly ILogger<DiscountService> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public DiscountService(ILogger<DiscountService> logger, IMediator mediator, IMapper mapper)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _mediator.Send(
            new GetDiscountQuery {ProductName = request.ProductName}, context.CancellationToken);
        // if (coupon == null)
        //     throw new RpcException(new Status(StatusCode.NotFound, $"Discount With ProductName={request.ProductName} " +
        //                                                            "is not found."));
        _logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);
        var couponModel = _mapper.Map<CouponModel>(coupon);
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var command = _mapper.Map<UpdateDiscountCommand>(request.Coupon);
        var coupon = await _mediator.Send(
            command, context.CancellationToken);

        _logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", command.ProductName);
        var couponModel = _mapper.Map<CouponModel>(coupon);
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request,
        ServerCallContext context)
    {
        var deleted = await _mediator.Send(
            new DeleteDiscountCommand {ProductName = request.ProductName}, context.CancellationToken);
        var response = new DeleteDiscountResponse
        {
            Success = deleted
        };
        return response;
    }
}