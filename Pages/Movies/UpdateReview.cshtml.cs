using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieReviews.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieReviews.Pages_Movies;

public class UpdateReviewModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly ILogger<UpdateReviewModel> _logger;

    [BindProperty]
    public Review Review {get; set;} = default!;
    public SelectList MoviesDropDown {get; set;} = default!;


    public UpdateReviewModel(AppDbContext context, ILogger<UpdateReviewModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult OnGet(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var review = _context.Reviews.Find(id);
        if (review == null)
        {
            return NotFound();
        }
        else
        {
            Review = review;
        }

        MoviesDropDown = new SelectList(_context.Movies.ToList(), "MovieID", "Title");
        return Page();
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

        _context.Attach(Review).State = EntityState.Modified;
        _context.SaveChanges();

        return RedirectToPage("./Index");
    }
}