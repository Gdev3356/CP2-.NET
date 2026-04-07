using Contra_Filé.Domain.Common;

namespace Contra_Filé.Domain;

public class Pedido : BaseEntity
{
    public decimal TotalValue { get; private set; }
    
    public Guid MesaId { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public List<Prato> Pratos { get; private set; }

    public Pedido(decimal totalValue, Guid mesaId, Guid userId)
    {
        if (totalValue < 0) throw new Exception("Valor total inválido.");
        
        TotalValue = totalValue;
        MesaId = mesaId;
        UserId = userId;
    }
}