using GameStore.Contracts.Genre;
using GameStore.UI.Models;

namespace GameStore.UI.Clients;

public class GenresClient
{
	private readonly string genreResourceName = Constants.GENRES_RESOURCE_NAME;

	private readonly HttpClient httpClient;

	public GenresClient(HttpClient httpClient)
	{
		this.httpClient = httpClient;
	}


	// --------------------------------------------------
	public async Task<List<Genre>> GetGenresAsync()
	{
		var allGenres = await this.httpClient.GetFromJsonAsync<List<Genre>>(genreResourceName);
		return allGenres ?? new List<Genre>();
	}
}
