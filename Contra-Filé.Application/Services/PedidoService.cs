using Contra_Filé.Application.DTO;
using Contra_Filé.Infrastructure.Persistence;

namespace Contra_Filé.Application.Services;

public class PedidoService : IPedidoService
{
    private readonly ContraFiléContext _context;

    public PedidoService(ContraFiléContext context) => _context = context;

    public IReadOnlyList<PedidoResponse> GetAll() =>
        _context.Pedidos.Select(PedidoResponse.FromDomain).ToList();

    public PedidoResponse? GetById(Guid id)
    {
        var pedido = _context.Pedidos.SingleOrDefault(p => p.Id == id);
        return pedido is null ? null : PedidoResponse.FromDomain(pedido);
    }

    public PedidoResponse Create(PedidoRequest request)
    {
        var pedido = request.ToDomain();
        _context.Pedidos.Add(pedido);
        _context.SaveChanges();
        return PedidoResponse.FromDomain(pedido);
    }

    public bool Delete(Guid id)
    {
        var pedido = _context.Pedidos.FirstOrDefault(p => p.Id == id);
        if (pedido is null) return false;
        _context.Pedidos.Remove(pedido);
        _context.SaveChanges();
        return true;
    }
}