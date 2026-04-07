using Contra_Filé.Domain.Common;
using Contra_Filé.Domain.Helpers;

namespace Contra_Filé.Domain;

public class User : BaseEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    
    private string PasswordSalt { get; set; }
    
    private DateOnly BirthDate { get;  set; } 
    
    public List<Avaliacao> Avaliacoes { get; private set; }
    
    public UserConfig Config { get; private set; }

    public User(string name, string email, DateOnly dateBorn, string rawPassword)
    {
        UpdateName(name);
        UpdateEmail(email);
        SetBirthDate(dateBorn);
        ChangePassword(rawPassword);
    }
    
    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new Exception("Nome não pode ser vazio.");
       
        if (newName.Length < 3 || newName.Length > 50)
            throw new Exception("O nome deve estar entre 10 e 50 caracteres.");
        
        Name = newName;
    }

    public void UpdateEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail) || !newEmail.Contains("@"))
            throw new Exception("E-mail inválido.");
            
        Email = newEmail;
    }

    public void SetBirthDate(DateOnly newDate)
    {
        var age = CalculateAge(newDate);
        
        if (age < 13)
            throw new Exception("Usuário deve ter pelo menos 13 anos.");

        BirthDate = newDate;
    }

    public void ChangePassword(string newRawPassword)
    {
        if (string.IsNullOrWhiteSpace(newRawPassword) || newRawPassword.Length < 8)
            throw new Exception("A senha deve ter pelo menos 8 caracteres.");

        PasswordSalt = Guid.NewGuid().ToString("N"); 
        
        Password = HashHelper.Hash(newRawPassword, PasswordSalt);
    }
    
    public int Age => CalculateAge(BirthDate);

    private static int CalculateAge(DateOnly date)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - date.Year;
        if (date > today.AddYears(-age)) age--;
        return age;
    } 
}