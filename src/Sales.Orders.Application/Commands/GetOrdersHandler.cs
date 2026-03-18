
using MediatR;
using Sales.Orders.Application.Common;
using Sales.Orders.Application.DTOs;
using Sales.Orders.Application.Queries;
using Sales.Orders.Domain.Interfaces;

namespace Sales.Orders.Application.Commands
{
    public class GetOrdersHandler
        : IRequestHandler<GetOrdersQuery, Result<PagedResult<OrderDto>>>
    {
        private readonly IOrderRepository _repository;

        public GetOrdersHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<PagedResult<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var (orders, totalCount) = await _repository.GetPagedAsync(request.Page, request.PageSize);

            var items = orders.Select(o => new OrderDto(
                o.Id,
                o.Customer.Name,
                o.Total,
                o.OrderStatus.ToString()
            )).ToList();

            var result = new PagedResult<OrderDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize
            };

            return Result<PagedResult<OrderDto>>.Success(result);
        }
    }
}
