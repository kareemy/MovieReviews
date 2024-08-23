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
    public class IndexModel : PageModel
    {
        private readonly MovieReviews.Models.AppDbContext _context;

        public IndexModel(MovieReviews.Models.AppDbContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Movie = await _context.Movies.Include(m => m.Reviews).ToListAsync();
        }
    }
}
