using System.ComponentModel.DataAnnotations;

namespace Soft_furniture.DataAccess.ViewModels.Delivers;

public class DeliverViewModel
{
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    public bool IsMale { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public DateOnly BirthDate { get; set; }

    public string Country { get; set; } = string.Empty;

    public string Region { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Description { get; set; } = String.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }


}
