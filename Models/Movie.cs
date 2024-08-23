using System.ComponentModel.DataAnnotations;

namespace MovieReviews.Models;

public class Movie
{
    public int MovieID {get; set;}

    [StringLength(60, MinimumLength = 3)]
    public string Title {get; set;} = string.Empty;

    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate {get; set;}

    [StringLength(30)]
    public string Genre {get; set;} = string.Empty;

    public string Description {get; set;} = string.Empty;

    public List<Review> Reviews {get; set;} = default!;
}