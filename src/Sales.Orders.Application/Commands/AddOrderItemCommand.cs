using MediatR;
using Sales.Orders.Application.Common;


namespace Sales.Orders.Application.Commands
{
    public record AddOrderItemCommand(
        Guid OrderId,
        CreateOrderItemCommand Item
    ) : IRequest<Result<Guid>>;

}
