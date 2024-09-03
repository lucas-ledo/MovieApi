using MovieApi.Models.TMDB;
using MovieApi.Services.ViewModels;

namespace MovieApi.Services
{
    public interface ITMDBService
    {
        Task<MovieViewModel?> SearchMovie(string query);
    }
}
