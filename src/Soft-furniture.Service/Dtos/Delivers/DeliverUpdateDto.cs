using System.ComponentModel.DataAnnotations;

namespace Soft_furniture.Service.Dtos.Delivers;

public class DeliverUpdateDto
{
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    public bool IsMale { get; set; }
    [MaxLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = String.Empty;

    public DateTime BirthDate { get; set; }

    [MaxLength(9)]
    public string PasspordSeriaNumber { get; set; } = String.Empty;

    public string Country { get; set; } = string.Empty;

    public string Region { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Description { get; set; } = String.Empty;
}
