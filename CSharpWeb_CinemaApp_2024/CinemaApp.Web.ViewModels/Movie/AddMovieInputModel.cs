using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Common;
using static CinemaApp.Common.EntityValidationConstants.Movie;
using static CinemaApp.Common.EntityValidationMessages.Movie;

namespace CinemaApp.Web.ViewModels.Movie
{
    public class AddMovieInputModel
    {
        public AddMovieInputModel()
        {
            this.ReleaseDate = DateTime.UtcNow.ToString(ReleaseDateFormat);
        }


        [Required(ErrorMessage = TitleRequiredMessage)] 
        [MaxLength(TitleMaxLength)] 
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = GenreRequiredMessage)]
        [MaxLength(GenreMaxLength)]
        public string Genre { get; set; } = null!;

        [Required(ErrorMessage = DirectorRequiredMessage)]
        [MaxLength(DirectorMaxLength)]
        public string Director { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = ReleaseDateRequiredMessage)]
        public string ReleaseDate { get; set; } = null!;

        [Required]
        [Range(MinDuration, MaxDuration)]
        public int Duration { get; set; }

    }
}
