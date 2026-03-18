
namespace Sales.Orders.Application.Queries
{
    using MediatR;
    using Sales.Orders.Application.Common;
    using Sales.Orders.Application.DTOs;

    public record GetOrdersQuery(int Page = 1, int PageSize = 10)
        : IRequest<Result<PagedResult<OrderDto>>>;
}
