
namespace Sales.Orders.Application.Queries
{
    using MediatR;
    using Sales.Orders.Application.Common;
    using Sales.Orders.Application.DTOs;

    public record GetPendingOrdersQuery()
        : IRequest<Result<List<OrderDto>>>;
}
