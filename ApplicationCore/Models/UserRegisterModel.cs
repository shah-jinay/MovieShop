using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class UserRegisterModel
{
    [Required, MaxLength(64)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required, MaxLength(64)]
    public string LastName { get; set; } = string.Empty;
    
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, Phone]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }
    
    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;
}
