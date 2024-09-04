using MovieApi.Models.TMDB;
using MovieApi.Providers;
using MovieApi.Services.ViewModels;

namespace MovieApi.Services
{
    public class TMDBService : ITMDBService
    {
        private readonly ITMDBProvider _tmdProvider;

        public TMDBService(ITMDBProvider tmdProvider)
        {
            _tmdProvider = tmdProvider;
        }

        public async Task<MovieViewModel?> SearchMovie(string query)
        {
            var movie = await _tmdProvider.SearchMovie(query);
            if (movie != null)
            {
                var recommendations = await _tmdProvider.RecommendationsMovie(movie.Id);
                var result = new MovieViewModel(movie);
                if (recommendations != null)
                {
                    foreach (var recommendation in recommendations.Take(5))
                    {
                        result.AddSimilarMovie(recommendation.Title, recommendation.ReleaseDate);
                    }
                }

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
