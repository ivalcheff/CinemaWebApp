

using System.ComponentModel.DataAnnotations;
using static CinemaApp.Common.EntityValidationConstants.Cinema;

namespace CinemaApp.Web.ViewModels.Cinema
{
    public class CinemaCreateViewModel
    {
        [Required]
        [MinLength(CinemaNameMinLength)]
        [MaxLength(CinemaNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(CinemaLocationMinLength)]
        [MaxLength(CinemaLocationMaxLength)]
        public string Location { get; set; } = null!;
    }
}
