
using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Data.Models
{
    public class Cinema
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public virtual ICollection<CinemaMovie> CinemaMovies { get; set; } = new HashSet<CinemaMovie>();

    }
}
