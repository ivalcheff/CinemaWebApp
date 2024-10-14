using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Web.ViewModels.Cinema
{
    public class MovieProgramViewModel
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public int Duration { get; set; }
    }
}
