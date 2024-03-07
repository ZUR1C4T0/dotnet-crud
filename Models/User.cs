using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Models;

[Index(nameof(Dni), IsUnique = true)]
public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public int Dni { get; set; }

    [Required]
    [StringLength(100)]
    public string Address { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;
}
