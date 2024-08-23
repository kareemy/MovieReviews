using System.ComponentModel.DataAnnotations;

namespace MovieReviews.Models;

public class Review
{
    public int ReviewID {get; set;}

    [Range(1,5)]
    public int Score {get; set;}

    [Display(Name = "Review Text")]
    public string ReviewText {get; set;} = string.Empty;

    [Display(Name = "Movie")]
    public int MovieID {get; set;}
    public Movie? Movie {get; set;} = default!;
}