using Contra_Filé.Domain.Common;

namespace Contra_Filé.Domain;

public class Mesa : BaseEntity
{
    public int Number { get; private set; }
    
    public bool IsOccupied { get; private set; }
    
    public List<Pedido> Pedidos { get; private set; }

    public Mesa(int number, bool isOccupied, List<Pedido> pedidos)
    {
        UpdateNumber(number); 
        IsOccupied = isOccupied;
        Pedidos = pedidos;
    }

    public void UpdateNumber(int number)
    {
        if (number < -1)
            throw new Exception("Não se pode ter mesa negativa!");
        Number = number;
    }
}