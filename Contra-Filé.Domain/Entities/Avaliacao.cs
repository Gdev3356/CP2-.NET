using Contra_Filé.Domain.Common;

namespace Contra_Filé.Domain;

public class Avaliacao : BaseEntity
{
    public Guid AvaliacaoId { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public string Description { get; private set; }
    
    public int Score { get; private set; }

    public Avaliacao(Guid avaliacaoId, Guid userId, int score, string description)
    {
        AvaliacaoId = avaliacaoId;
        UserId = userId;
        UpdateScore(score);
        UpdateDescription(description);
    }

    public void UpdateScore(int score)
    {
        if (score is < 1 or > 5) throw new Exception("A avaliação deve ter valor entre 1 e 5");
        Score = score;
    }

    public void UpdateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description)) 
            throw new Exception("A descrição não pode ser vazia.");
        
        if (description.Length < 12 || description.Length > 100)
            throw new Exception("A descrição deve ter entre 12 e 100 caracteres.");

        Description = description;
    }
}