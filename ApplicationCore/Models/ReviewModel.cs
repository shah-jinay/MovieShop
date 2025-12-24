namespace ApplicationCore.Models;

public class ReviewModel
{
    public string UserName { get; set; } = "MovieShop User";
    public decimal Rating { get; set; }
    public string ReviewText { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}
