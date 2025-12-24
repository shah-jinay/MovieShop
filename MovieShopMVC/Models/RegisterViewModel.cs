using System.ComponentModel.DataAnnotations;

namespace MovieShopMVC.Models;

public class RegisterViewModel
{
    [Required, MaxLength(64)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(64)]
    public string LastName { get; set; } = string.Empty;

    [Required, Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(6), DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required, DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
