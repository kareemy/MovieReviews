using Microsoft.EntityFrameworkCore;

namespace MovieReviews.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
    {

    }

    public DbSet<Movie> Movies {get; set;}
    public DbSet<Review> Reviews {get; set;}
}