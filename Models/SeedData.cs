using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace MovieReviews.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

        if (context.Movies.Any()) 
        {
            return;
        }

        context.Movies.AddRange(
            new Movie { Title = "Inside Out 2", ReleaseDate = DateTime.Parse("06/14/2024"), Genre = "Kids & Family", Description = "The little voices inside Riley's head know her inside and out--but next summer, everything changes when Disney and Pixar's Inside Out 2 introduces a new Emotion: Anxiety." },
            new Movie { Title = "Deadpool & Wolverine", ReleaseDate = DateTime.Parse("07/26/2024"), Genre = "Action/Adventure", Description = "Deadpool's peaceful existence comes crashing down when the Time Variance Authority recruits him to help safeguard the multiverse. He soon unites with his would-be pal, Wolverine, to complete the mission and save his world from an existential threat. " },
            new Movie { Title = "Dune: Part Two", ReleaseDate = DateTime.Parse("03/01/2024"), Genre = "Sci-Fi", Description = "Dune: Part Two will explore the mythic journey of Paul Atreides as he unites with Chani and the Fremen while on a warpath of revenge against the conspirators who destroyed his family. Facing a choice between the love of his life and the fate of the known universe, he endeavors to prevent a terrible future only he can foresee. " },
            new Movie { Title = "Argylle", ReleaseDate = DateTime.Parse("02/2/2024"), Genre = "Drama", Description = "When the plots of reclusive author Elly Conway's fictional espionage novels begin to mirror the covert actions of a real-life spy organization, quiet evenings at home become a thing of the past. Accompanied by her cat Alfie and Aiden, a cat-allergic spy, Elly races across the world to stay one step ahead of the killers as the line between Conway's fictional world and her real one begins to blur." }
        );

        context.SaveChanges();

        // Add a movie review for inside out 2
        Review InsideOutReview = new Review {
            Score = 5,
            ReviewText = "Inside Out 2 is a brilliantly crafted sequel that delves deeper into the emotional landscape of adolescence. The film masterfully balances humor and heart, introducing new emotions like Anxiety, Envy, and Embarrassment, which complicate Riley's journey through high school. The interactions between old and new emotions provide a fresh, insightful look at the tumultuous nature of growing up. With stunning animation, a compelling storyline, and a pitch-perfect voice cast, Inside Out 2 is a powerful exploration of self-identity and the complexities of teenage life.",
            Movie = context.Movies.Where(m => m.Title == "Inside Out 2").Single()
        };
        context.Add(InsideOutReview);
        context.SaveChanges();
    }
}