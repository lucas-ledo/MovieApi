namespace MovieApi.Providers
{
    public interface ITMDBProvider
    {
        Task<T?> SearchMovie<T>(string query);
        Task<List<T>?> RecommendationsMovie<T>(int id);
    }
}
