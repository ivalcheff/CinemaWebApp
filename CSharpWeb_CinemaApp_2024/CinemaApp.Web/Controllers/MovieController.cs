using System.Globalization;
using CinemaApp.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Web.ViewModels.Cinema;
using CinemaApp.Web.ViewModels.Movie;
using static CinemaApp.Common.EntityValidationConstants.Movie;

namespace CinemaApp.Web.Controllers
{
    public class MovieController : BaseController
    {

        private readonly CinemaDbContext dbContext;

        public MovieController(CinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Movie> allMovies = await this.dbContext.Movies.ToListAsync();

            return View(allMovies);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //зарежда формата за създаване на нов обект в колекцията
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddMovieInputModel inputModel)
        {

            bool IsReleaseDateValid = DateTime.TryParseExact(inputModel.ReleaseDate, ReleaseDateFormat, 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDate);

            if (!IsReleaseDateValid)
            {
                this.ModelState.AddModelError(nameof(inputModel.ReleaseDate), $"The release date must be in the following format: {ReleaseDateFormat}");
                return this.View(inputModel);
            }

            if (!this.ModelState.IsValid)
            {
                //render the same form with user entered data with errors
                return this.View(inputModel);
            }

            Movie movie = new Movie()
            {
                Title = inputModel.Title,
                Genre = inputModel.Genre,
                ReleaseDate = releaseDate,
                Director = inputModel.Director,
                Duration = inputModel.Duration,
                Description = inputModel.Description
            };

            await this.dbContext.Movies.AddAsync(movie);
            await this.dbContext.SaveChangesAsync();

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(id, ref movieGuid);
            if (!isGuidValid)
            {
                //checks for invalid id format
                return this.RedirectToAction(nameof(Index));
            }

            Movie? movie = await this.dbContext.Movies.FirstOrDefaultAsync(m => m.Id == movieGuid);

            if (movie == null)
            {
                //checks if such a movie exists in db
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(movie);
        }


        [HttpGet]

        public async Task<IActionResult> AddToProgram(string? id)
        {
            Guid movieGuid = Guid.Empty;
            bool isMovieGuidValid = this.IsGuidValid(id, ref movieGuid);
            if (!isMovieGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            Movie? movie = await this.dbContext.Movies.FirstOrDefaultAsync(m => m.Id == movieGuid);

            if (movie == null)
            {
                //checks if such a movie exists in db
                return this.RedirectToAction(nameof(Index));
            }

            AddMovieToCinemaProgramInputModel viewModel = new AddMovieToCinemaProgramInputModel()
            {
                MovieId = movie.Id.ToString(),
                MovieTitle = movie.Title,
                Cinemas = await this.dbContext
                    .Cinemas
                    .Include(c => c.CinemaMovies)
                    .ThenInclude(cm => cm.Movie)
                    .Select(c => new CinemaCheckBoxItem()
                    {
                        Id = c.Id.ToString(),
                        Name = c.Name,
                        Location = c.Location,
                        IsSelected = c.CinemaMovies
                            .Any(cm => cm.Movie.Id == movieGuid)
                    })
                    .ToArrayAsync()
            };

            return this.View(viewModel);
        }

        [HttpPost]

        public async Task<IActionResult> AddToProgram(AddMovieToCinemaProgramInputModel model)
        {
            //validate the model 
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            //verify the movie id
            Guid movieGuid = Guid.Empty;

            bool isMovieIdValid = this.IsGuidValid(model.MovieId, ref movieGuid);
            if (!isMovieIdValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            //check if there's a movie with this guid
            Movie? movie = await this.dbContext
                .Movies
                .FirstOrDefaultAsync(m => m.Id == movieGuid);

            if (movie == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            ICollection<CinemaMovie> entitiesToAdd = new List<CinemaMovie>();

            foreach (CinemaCheckBoxItem cinemaInputModel in model.Cinemas)
            {

                Guid cinemaGuid = Guid.Empty;

                bool isCinemaIdValid = this.IsGuidValid(cinemaInputModel.Id, ref cinemaGuid);
                if (!isCinemaIdValid)
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid cinema selected");
                    return this.View(model);
                }

                Cinema? cinema = await this.dbContext
                    .Cinemas
                    .FirstOrDefaultAsync(c => c.Id == cinemaGuid);

                if (cinema == null)
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid cinema selected");
                    return this.View(model);
                }


                if (cinemaInputModel.IsSelected)
                {
                    entitiesToAdd.Add(new CinemaMovie()
                    {
                        Cinema = cinema,
                        Movie = movie
                    });
                }
                else
                {
                    // TODO implement deletion
                }

            }

            await this.dbContext.CinemasMovies.AddRangeAsync(entitiesToAdd);
            await this.dbContext.SaveChangesAsync();

            return this.RedirectToAction(nameof(Index));
        }


    }
}
