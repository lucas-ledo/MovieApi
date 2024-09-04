using MovieApi.Models.TMDB;

namespace MovieApi.Providers
{
    public interface ITMDBProvider
    {
        Task<MovieTMDB?> SearchMovie(string query);
        Task<List<MovieTMDB>?> RecommendationsMovie(int id);
    }
}
