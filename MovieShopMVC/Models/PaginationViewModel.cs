namespace MovieShopMVC.Models;

public class PaginationLink
{
    public int? PageNumber { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsCurrent { get; set; }
    public bool IsEllipsis { get; set; }
}

public class PaginationViewModel
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public string? PreviousPageUrl { get; set; }
    public string? NextPageUrl { get; set; }
    public List<PaginationLink> Links { get; set; } = new();
}
