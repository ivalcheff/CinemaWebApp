using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Web.ViewModels.Cinema;
using static CinemaApp.Common.EntityValidationConstants.Movie;

namespace CinemaApp.Web.ViewModels.Movie
{
    public class AddMovieToCinemaProgramInputModel
    {
        [Required]
        public string MovieId { get; set; } = null!;

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string MovieTitle { get; set; } = null!;
        public IList<CinemaCheckBoxItem> Cinemas { get; set; } = new List<CinemaCheckBoxItem>();
    }
}
