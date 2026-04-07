using Contra_Filé.Domain.Entities;

namespace Contra_Filé.Application.DTO;

public record PedidoRequest(Guid MesaId, Guid UserId, List<Guid> PratoIds)
{
    public Pedido ToDomain() => new Pedido(0m, MesaId, UserId);
}