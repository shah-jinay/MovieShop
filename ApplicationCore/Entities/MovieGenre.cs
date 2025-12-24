using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

public class MovieGenre
{
    public int GenreId { get; set; }
    public int MovieId { get; set; }
    
    //Navigation Property
    public Movie Movie { get; set; }
    public Genre Genre { get; set; }
}