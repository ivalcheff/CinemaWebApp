using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Common
{
    public static class EntityValidationConstants
    {

        public static class Movie
        {
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 100;
            public const int GenreMinLength = 3;
            public const int GenreMaxLength = 50;
            public const int DirectorMinLength = 3;
            public const int DirectorMaxLength = 50;
            public const int DescriptionMinLength = 20;
            public const int DescriptionMaxLength = 500;
            public const int MinDuration = 1;
            public const int MaxDuration = 999;
            public const string ReleaseDateFormat = "yyyy-MM";
        }

        public static class Cinema
        {
            public const int CinemaNameMinLength = 3;
            public const int CinemaNameMaxLength = 100;
            public const int CinemaLocationMinLength = 3;
            public const int CinemaLocationMaxLength = 85;

        }

    }
}
