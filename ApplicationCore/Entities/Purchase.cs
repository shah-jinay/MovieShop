namespace ApplicationCore.Entities;

public class Purchase
{

    public int MovieId { get; set; }
    public int UserId { get; set; }

    public DateTime PurchaseDateTime { get; set; }
    public Guid PurchaseNumber { get; set; }
    public decimal TotalPrice { get; set; }
    
    
    public Movie? Movie { get; set; }
    public User? User { get; set; }
    

}