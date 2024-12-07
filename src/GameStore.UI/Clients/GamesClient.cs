using GameStore.Contracts.Game;
using GameStore.UI.Models;

namespace GameStore.UI.Clients;

public class GamesClient
{
	private readonly string gamesResourceName = Constants.GAMES_RESOURCE_NAME;

	private readonly HttpClient httpClient;


	public GamesClient(HttpClient httpClient)
	{
		this.httpClient = httpClient;
	}


	// --------------------------------------------------
	public async Task<List<GameSummary>> GetGamesAsync()
	{
		var games = await this.httpClient.GetFromJsonAsync<List<GameSummary>>(gamesResourceName);
		return games ?? new List<GameSummary>();
	}

	public async Task AddGameAsync(GameDetails gameDetails)
	{
		await this.httpClient.PostAsJsonAsync(gamesResourceName, gameDetails);
	}

	public async Task<GameDetails> GetGameAsync(int id)
	{
		var gameDetails = await this.httpClient.GetFromJsonAsync<GameDetails>($"{gamesResourceName}/{id}");
		return gameDetails ?? throw new Exception("Game not found!");
	}

	public async Task UpdateGameAsync(GameDetails updatedGameDetails)
	{
		await this.httpClient.PutAsJsonAsync($"{gamesResourceName}/{updatedGameDetails.Id}", updatedGameDetails);
	}

	public async Task DeleteGameByIdAsync(int? id)
	{
		await this.httpClient.DeleteAsync($"{gamesResourceName}/{id}");
	}
}
