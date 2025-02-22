﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Models
{
    public class CinemaMovie
    {
        public Guid MovieId { get; set; } //no need to initialize, as these Guids are only references
        public virtual Movie Movie { get; set; } = null!;

        public Guid CinemaId { get; set; } 
        public virtual Cinema Cinema { get; set; } = null!;
    }
}
