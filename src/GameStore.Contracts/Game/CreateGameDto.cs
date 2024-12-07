namespace GameStore.Contracts.Game;

public record CreateGameDto(
	string Name,
	int GenreId,
	decimal Price,
	DateOnly ReleaseDate);
