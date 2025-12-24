namespace ApplicationCore.Models;

public class PurchasedMovieModel
{
    public MovieCardModel Movie { get; set; } = new();
    public DateTime PurchaseDateTime { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid PurchaseNumber { get; set; }
}
