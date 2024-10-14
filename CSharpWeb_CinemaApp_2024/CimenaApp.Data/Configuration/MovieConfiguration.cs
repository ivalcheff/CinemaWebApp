

namespace CinemaApp.Data.Configuration
{

    using CinemaApp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static Common.EntityValidationConstants.Movie;

    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            //Fluent API

            builder.HasKey(m => m.Id);
            builder
                .Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(TitleMaxLength);

            builder.Property(m => m.Genre)
                .IsRequired()
                .HasMaxLength(GenreMaxLength);

            builder.Property(m => m.Director)
                .IsRequired()
                .HasMaxLength(DirectorMaxLength);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            builder.HasData(SeedMovies());

        }

        private List<Movie> SeedMovies()
        {
            List<Movie> movies = new List<Movie>()
            {
                new Movie()
                {
                    Title = "Harry Potter and the Goblet of Fire",
                    Genre = "Fantasy",
                    ReleaseDate = new DateTime (2005, 11,01),
                    Director = "Mike Newel",
                    Duration = 157,
                    Description = "The fourth movie in the Harry Potter franchise sees Harry (Daniel Radcliffe) returning for his fourth year at Hogwarts School of Witchcraft and Wizardry, along with his friends, Ron (Rupert Grint) and Hermione (Emma Watson). There is an upcoming tournament between the three major schools of magic, with one participant selected from each school by the Goblet of Fire. When Harry's name is drawn, even though he is not eligible and is a fourth player, he must compete in the dangerous contest."
                },

                new Movie()
                {
                    Title = "The Lord of The Rings: The Fellowship of the Ring",
                    Genre = "Fantasy",
                    ReleaseDate = new DateTime(2001,12,19),
                    Director = "Peter Jackson",
                    Duration = 178,
                    Description = "The future of civilization rests in the fate of the One Ring, which has been lost for centuries. Powerful forces are unrelenting in their search for it. But fate has placed it in the hands of a young Hobbit named Frodo Baggins (Elijah Wood), who inherits the Ring and steps into legend. A daunting task lies ahead for Frodo when he becomes the Ringbearer - to destroy the One Ring in the fires of Mount Doom where it was forged."
                }

            };

            return movies;
        }
    }
}
