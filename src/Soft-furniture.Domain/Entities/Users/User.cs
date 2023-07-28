using Soft_furniture.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Soft_furniture.Domain.Entities.Users;

public class User : Human
{

    [MaxLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;

    public bool PhoneNumberConfirmed { get; set; }

    public string PasswordHash { get; set; } = string.Empty;

    public string Salt { get; set; } = string.Empty;

    public IdentityRole IdentityRole { get; set; }
}
