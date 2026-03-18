

using MediatR;
using Sales.Orders.Application.Common;

namespace Sales.Orders.Application.Commands
{
    public record UpdateOrderCommand(
        Guid Id,
        Guid CustomerId,
        string CustomerName,
        Guid CompanyId,
        string CompanyName
    ) : IRequest<Result<bool>>;
}
