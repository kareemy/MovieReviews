using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieReviews.Models;

namespace MovieReviews.Pages_Movies
{
    public class DetailsModel : PageModel
    {
        private readonly MovieReviews.Models.AppDbContext _context;

        public DetailsModel(MovieReviews.Models.AppDbContext context)
        {
            _context = context;
        }

        public Movie Movie { get; set; } = default!;

        [BindProperty]
        public int ReviewIDToDelete {get; set;}

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.Include(m => m.Reviews).FirstOrDefaultAsync(m => m.MovieID == id);
            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                Movie = movie;
            }
            return Page();
        }

        public IActionResult OnPostDeleteReview(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var review = _context.Reviews.FirstOrDefault(r => r.ReviewID == ReviewIDToDelete);
            if (review != null)
            {
                _context.Remove(review);
                _context.SaveChanges();
            }

            Movie = _context.Movies.Include(m => m.Reviews).First(m => m.MovieID == id);
            return Page();
        }
    }
}
