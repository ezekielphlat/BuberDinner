
namespace BuberDinner.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid(); //creates a new guid if the id is not specified
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

