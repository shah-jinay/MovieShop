namespace ApplicationCore.Entities;

public class User
{
    public int Id { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string HashesPassword { get; set; }
    public string isLocked { get; set; }
    public string PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? Salt { get; set; }
    
    //Navigation Property
    public ICollection<Favorite>? Favorites { get; set; }
    public ICollection<Review>? Reviews { get; set; }
    public ICollection<Purchase>? Purchases { get; set; }
    public ICollection<UserRole>? UserRoles { get; set; }

}