using System.ComponentModel.DataAnnotations;

namespace Soft_furniture.DataAccess.ViewModels.Users;

public class UserViewModel
{
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string Region { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public DateTime UpdatedAt { get; set; }

}
