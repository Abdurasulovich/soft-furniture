namespace Soft_furniture.Service.Dtos.Auth;

public class RegisterDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Password_hash { get; set; } = string.Empty;

    public string Country { get; set; } = String.Empty;

    public string Region { get; set; } = String.Empty;

    public string City { get; set; } = String.Empty;

    public string Address { get; set; } = String.Empty;

}
