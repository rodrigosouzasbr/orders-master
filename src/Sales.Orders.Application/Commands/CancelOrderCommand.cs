
using MediatR;
using Sales.Orders.Application.Common;

namespace Sales.Orders.Application.Commands
{
    public record CancelOrderCommand(Guid OrderId)
        : IRequest<Result<bool>>;
}
