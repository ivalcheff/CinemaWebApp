using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static CinemaApp.Common.EntityValidationConstants.Cinema;

namespace CinemaApp.Data.Configuration
{
    public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(CinemaNameMaxLength);

            builder
                .Property(c => c.Location)
                .IsRequired()
                .HasMaxLength(CinemaLocationMaxLength);

            builder.HasData(this.GenerateCinemas());
        }

        private IEnumerable<Cinema> GenerateCinemas()
        {
            IEnumerable<Cinema> cinemas = new List<Cinema>()
            {
                new Cinema()
                {
                    Name = "Cinema City",
                    Location = "Plovdiv"
                },

                new Cinema()
                {
                    Name = "Regal",
                    Location = "Irvine"
                },
                new Cinema()
                {
                    Name = "CineLand",
                    Location = "Sofia"
                }

            };
            return cinemas;
        }
    }
}
