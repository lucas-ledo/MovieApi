using MovieApi.Models.TMDB;

namespace MovieApi.Services.ViewModels
{
    public class MovieViewModel
    {
        public string? Title { get; set; }
        public string? OriginalTitle { get; set; }
        public double VoteAverage { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Overview { get; set; }
        public List<string> SimilarMovies { get; set; } = new List<string>();


        public void AddSimilarMovie(string title, DateTime releaseDate)
        {
            if (SimilarMovies.Count < 5)
            {
                SimilarMovies.Add($"{title} ({releaseDate.Year})");
            }
        }

        public MovieViewModel(MovieTMDB movieTMDB)
        {
            Title = movieTMDB.Title ?? "Desconocido";
            OriginalTitle = movieTMDB.OriginalTitle ?? "Desconocido";
            VoteAverage = movieTMDB.VoteAverage;
            ReleaseDate = movieTMDB.ReleaseDate;
            Overview = movieTMDB.Overview ?? "Desconocido";
            SimilarMovies = new List<string>();
        }
    }
}
