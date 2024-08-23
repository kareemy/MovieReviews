using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieReviews.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MovieReviews.Pages_Movies;

public class DeleteReviewModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly ILogger<DeleteReviewModel> _logger;

    // Drop down list of all the Movie Reviews
    public SelectList Reviews {get; set;} = default!;

    // ReviewId to delete. We bind this property because the user will select it in our form and submit it.
    [BindProperty]
    [Display(Name = "Review")]
    public int? ReviewID {get; set;}

    public DeleteReviewModel(AppDbContext context, ILogger<DeleteReviewModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult OnGet(int? id)
    {
        // Get all the reviews to populate our SelectList drop down
        // Use an anonymous type because we want a new variable that shows the Movie Title and Review score
        var reviewsWithTitles = _context.Reviews.Include(r => r.Movie).Select(r => new {
            ID = r.ReviewID,
            Display = $"{r.Movie!.Title}: {r.Score} out of 5"
        });
        _logger.LogInformation($"DeleteReview OnGet() called. ReviewId = '{ReviewID}'. id = '{id}'");

        // Populate SelectList. This variable is brought into the Razor Page with the asp-items tag helper
        // The final `id` parameter indicates which entry should be selected by default
        Reviews = new SelectList(reviewsWithTitles.ToList(), "ID", "Display", id);
        return Page();
    }

    
    public IActionResult OnPost()
    {
        _logger.LogInformation($"DeleteReview OnPost() called. ReviewId = '{ReviewID}'.");

        if (ReviewID == null)
        {
            return NotFound();
        }
        // Find the review in the database
        var review = _context.Reviews.Find(ReviewID);

        if (review != null)
        {
            _context.Reviews.Remove(review); // Delete the review
            _context.SaveChanges();
        }

        return RedirectToPage("./Index");
    }
}
