using Microsoft.AspNetCore.Identity;
using Soft_furniture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soft_furniture.Service.Dtos.Users;

public class UserCreateDto
{
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;

    public string Password_hash { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string Region { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;


}
