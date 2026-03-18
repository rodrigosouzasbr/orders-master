

using MediatR;
using Sales.Orders.Application.Common;

namespace Sales.Orders.Application.Commands
{
    public record RemoveOrderItemCommand(
        Guid OrderId,
        Guid ItemId
    ) : IRequest<Result<bool>>;
}
