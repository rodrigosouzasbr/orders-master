using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.Orders.Application.Commands;
using Sales.Orders.Application.Queries;

namespace Sales.Orders.Api.Controllers;

[ApiController]
[Route("order/api")]
public class OrderController : ControllerBase
{

    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateOrderCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }


    [Authorize]
    [HttpPost("{orderId}/AddItem")]
    public async Task<IActionResult> AddItem(Guid orderId, AddOrderItemCommand command)
    {
        command = command with { OrderId = orderId };

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet("pending")]
    public async Task<IActionResult> GetPending()
    {
        var result = await _mediator.Send(new GetPendingOrdersQuery());

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetOrderByIdQuery(id));

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetOrdersQuery(page, pageSize));

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateOrderCommand command)
    {
        command = command with { Id = id };

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return NoContent();
    }

    [Authorize]
    [HttpPut("{orderId}/items/{itemId}")]
    public async Task<IActionResult> UpdateItem(
    Guid orderId,
    Guid itemId,
    UpdateOrderItemCommand command)
    {
        command = command with { OrderId = orderId, ItemId = itemId };

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{orderId}/items/{itemId}")]
    public async Task<IActionResult> RemoveItem(Guid orderId, Guid itemId)
    {
        var command = new RemoveOrderItemCommand(orderId, itemId);

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }


    [Authorize]
    [HttpPatch("{orderId}/cancel")]
    public async Task<IActionResult> Cancel(Guid orderId)
    {
        var command = new CancelOrderCommand(orderId);

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }

}