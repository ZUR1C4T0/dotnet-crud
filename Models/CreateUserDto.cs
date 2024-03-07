
using System.ComponentModel.DataAnnotations;

namespace dotnet.Models;

public class CreateUserDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public int Dni { get; set; }

    [Required]
    public string Address { get; set; } = string.Empty;

    [Required]
    public string Phone { get; set; } = string.Empty;

    public User ToUser()
    {
        return new User
        {
            Name = Name,
            LastName = LastName,
            Dni = Dni,
            Address = Address,
            Phone = Phone
        };
    }
}
