using Microsoft.AspNetCore.Mvc;
using Contra_Filé.Application.Services;
using Contra_Filé.Application.DTO;

namespace Contra_Filé.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService) => _pedidoService = pedidoService;

    [HttpGet]
    public IActionResult GetAll() => Ok(_pedidoService.GetAll());

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var pedido = _pedidoService.GetById(id);
        return pedido is null ? NotFound() : Ok(pedido);
    }

    [HttpPost]
    public IActionResult Create([FromBody] PedidoRequest request)
    {
        var pedido = _pedidoService.Create(request);
        return Ok(pedido);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id) =>
        _pedidoService.Delete(id) ? NoContent() : NotFound();
}