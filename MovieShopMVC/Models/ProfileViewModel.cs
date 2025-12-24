using System.ComponentModel.DataAnnotations;

namespace MovieShopMVC.Models;

public class ProfileViewModel
{
    public int Id { get; set; }

    [Required, MaxLength(64)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(64)]
    public string LastName { get; set; } = string.Empty;

    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }

    public string Email { get; set; } = string.Empty;
}
