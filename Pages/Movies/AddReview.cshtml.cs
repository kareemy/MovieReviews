using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieReviews.Models;

namespace MovieReviews.Pages_Movies;

public class AddReviewModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly ILogger<AddReviewModel> _logger;

    [BindProperty]
    public Review Review {get; set;} = default!;
    public SelectList MoviesDropDown {get; set;} = default!;

    public AddReviewModel(AppDbContext context, ILogger<AddReviewModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void OnGet()
    {
        MoviesDropDown = new SelectList(_context.Movies.ToList(), "MovieID", "Title");
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var e in allErrors)
            {
                _logger.LogError($"Error: {e.ErrorMessage}");
            }
            return Page();
        }

        _context.Reviews.Add(Review);
        _context.SaveChanges();

        return RedirectToPage("./Index");
    }
}
