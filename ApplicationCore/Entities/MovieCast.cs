namespace ApplicationCore.Entities;

public class MovieCast
{
    public int CastId { get; set; }
    public string Character { get; set; }
    public int MovieId { get; set; }
    
    
    //Navigation Property
    public Movie Movie { get; set; }
    public Cast Cast { get; set; }
}