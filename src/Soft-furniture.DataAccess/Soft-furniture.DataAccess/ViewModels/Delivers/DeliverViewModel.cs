using System.ComponentModel.DataAnnotations;

namespace Soft_furniture.DataAccess.ViewModels.Delivers;

public class DeliverViewModel
{
    [MaxLength(50)]
    public string FirstName { get; set; } = String.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = String.Empty;

    public bool IsMale { get; set; }

    public string PhoneNumber { get; set; } = String.Empty;

    public DateOnly BirthDate { get; set; }

    public string Country { get; set; } = String.Empty;

    public string Region { get; set; } = String.Empty;

    public string City { get; set; } = String.Empty;

    public string Address { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }


}
