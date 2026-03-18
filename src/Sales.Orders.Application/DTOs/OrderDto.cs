

using System.ComponentModel.DataAnnotations;

namespace Sales.Orders.Application.DTOs
{
    public record OrderDto(
        Guid Id,

        [property: Required]
        [property: MaxLength(100)]
        string CustomerName,

        [property: Range(0, double.MaxValue)]
        decimal Total,

        [property: Required]
        string Status
    );
}
