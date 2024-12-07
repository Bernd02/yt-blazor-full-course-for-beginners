namespace GameStore.Contracts.Game;

public record UpdateGameDto(
	string Name,
	int GenreId,
	decimal Price,
	DateOnly ReleaseDate);
