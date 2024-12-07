using GameStore.Contracts.Genre;
using GameStore.Data.Models;

namespace GameStore.Services.Extentsions;

public static class GenreMappingExtensions
{
	public static GenreDto ToGenreDto(this Genre genre)
	{
		return new GenreDto(
			genre.Id,
			genre.Name);
	}
}
