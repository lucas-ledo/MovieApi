using Microsoft.AspNetCore.DataProtection.KeyManagement;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Text.Json;
using MovieApi.Models.TMDB;

namespace MovieApi.Providers
{
    public class TMDBProvider : ITMDBProvider
    {

        private const string BEARER = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJiNzdkMTgwOTgwZDRlOWRiZWJkNWQ5YThiZTk2Y2UxYiIsIm5iZiI6MTcyNTM3NzM4Mi43MDAxNTQsInN1YiI6IjY0YTViYmI4MTU4Yzg1MDBjNWM2MTBmNCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.yWxq5GwIs9edrJWGjGVtIOq987o3WjRx7IVaS1s_WUw";
        private const string BASE_URL = "https://api.themoviedb.org/3";
        private readonly Dictionary<string, object> _cache = new(); //Recomendable Redis

        public async Task<MovieTMDB?> SearchMovie(string query)
        {
            if (_cache.TryGetValue("search-movie-" + query, out var cachedResult))
            {
                return (MovieTMDB)cachedResult;
            }
            using (var client = new HttpClient())
            {
                string url = $"{BASE_URL}/search/movie?language=es-ES&page=1&query=${Uri.EscapeDataString(query)}";

                AddHeadersTMDB(client);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var queryResult = JsonSerializer.Deserialize<SearchResultTMDB<List<MovieTMDB>>>(responseBody);
                    if (queryResult != null)
                    {
                        var result = queryResult.Results?.FirstOrDefault();

                        _cache["search-movie-" + query] = result;

                        return result;
                    }
                    else
                    {
                        return default;
                    }

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Error en la solicitud: " + e.Message);
                }
                return default;
            }
        }

        public async Task<List<MovieTMDB>?> RecommendationsMovie(int id)
        {
            if (_cache.TryGetValue("recommendations-movie-" + id, out var cachedResult))
            {
                return (List<MovieTMDB>?)cachedResult;
            }
            using (var client = new HttpClient())
            {
                string url = $"{BASE_URL}/movie/{id}/recommendations?language=es-ES&page=1";

                AddHeadersTMDB(client);

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var queryResult = JsonSerializer.Deserialize<SearchResultTMDB<List<MovieTMDB>>>(responseBody);
                    if (queryResult != null)
                    {
                        var result = queryResult.Results;

                        _cache["recommendations-movie-" + id] = result;

                        return result;
                    }
                    else
                    {
                        return default;
                    }

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Error en la solicitud: " + e.Message);
                }
                return default;
            }
        }

        private void AddHeadersTMDB(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {BEARER}");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }
    }
}
