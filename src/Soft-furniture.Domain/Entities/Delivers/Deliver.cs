using System.ComponentModel.DataAnnotations;

namespace Soft_furniture.Domain.Entities.Delivers;

public class Deliver : Human
{
    public bool IsMale { get; set; }
    [MaxLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;

    public bool PhoneNumberConfirmed { get; set; }

    public string PasswordHash { get; set; } = String.Empty;

    public string Salt { get; set; } = String.Empty;

    public DateTime BirthDate { get; set; }

    [MaxLength(9)]
    public string PasspordSeriaNumber { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

}
