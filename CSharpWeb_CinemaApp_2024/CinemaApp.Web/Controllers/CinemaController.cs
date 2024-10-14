using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Web.ViewModels.Cinema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CinemaApp.Common.EntityValidationConstants.Cinema;

namespace CinemaApp.Web.Controllers
{
    public class CinemaController : BaseController
    {
        private readonly CinemaDbContext dbContext;

        public CinemaController(CinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CinemaIndexViewModel> cinemas = 
                await this.dbContext.Cinemas
                .Select(c => new CinemaIndexViewModel()
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Location = c.Location
                })
                .OrderBy(c => c.Location)
                .ThenBy(c => c.Name)
                .ToArrayAsync();

            return this.View(cinemas);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult>Create (CinemaCreateViewModel inputModel)
        {
            //validate the model first
            if (!this.ModelState.IsValid)
            {
                //return the form with completed data 
                return this.View(inputModel);
            }

            Cinema cinema = new Cinema()
            {
                Name = inputModel.Name,
                Location = inputModel.Location
            };
            
            await this.dbContext.Cinemas.AddAsync(cinema);
            await this.dbContext.SaveChangesAsync();

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            Guid cinemaGuid = Guid.Empty;

            bool isIdValid = this.IsGuidValid(id, ref cinemaGuid);

            if (!isIdValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            Cinema? cinema = await this.dbContext
                .Cinemas
                .Include(c => c.CinemaMovies)
                .ThenInclude(cm => cm.Movie)
                .FirstOrDefaultAsync(c => c.Id == cinemaGuid);

            if (cinema == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            //create a view model from the existing cinema
            var cinemaDetailsViewModel = new CinemaDetailsViewModel
            {
                Id = cinema.Id.ToString(),
                Name = cinema.Name,
                Location = cinema.Location,
                Movies = cinema.CinemaMovies
                    .Select(cm => new MovieProgramViewModel
                    {
                        Title = cm.Movie.Title,
                        Duration = cm.Movie.Duration
                    }).ToList()
            };

            return this.View(cinemaDetailsViewModel);
        }

        private bool IsCinemaIdValid(string id, ref Guid cinemaGuid)
        {
            //non-existing parameter in the URL
            if (String.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            //invalid parameter int he URL
            bool isGuidValid = Guid.TryParse(id, out cinemaGuid);
            if (!isGuidValid)
            {
                return false;
            }

            return true;
        }
    }
}
