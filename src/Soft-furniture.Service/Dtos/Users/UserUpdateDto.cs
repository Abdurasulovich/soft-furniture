using Microsoft.AspNetCore.Identity;
using Soft_furniture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soft_furniture.Service.Dtos.Users;

public class UserUpdateDto : Human
{

    [MaxLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;

    public bool PhoneNumberConfirmed { get; set; }

    public string Password_hash { get; set; } = string.Empty;

    public string Salt { get; set; } = string.Empty;

    public IdentityRole Role { get; set; }
}
