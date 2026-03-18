

namespace Sales.Orders.Application.Queries
{
    using MediatR;
    using Sales.Orders.Application.Common;
    using Sales.Orders.Application.DTOs;

    public record GetOrderByIdQuery(Guid Id)
        : IRequest<Result<OrderDto>>;


}
