using GameStore.Contracts.Genre;
using GameStore.Services.Extentsions;
using GameStore.Services.GenreService;

namespace GameStore.Api.Endpoints;

public static class GenreEndpoints
{
	public static void MapGenresEndpoints(this WebApplication app)
	{
		var genreGroup = app.MapGroup(Constants.GENRES_RESOURCE_NAME);

		// --------------------------------------------------
		// GET /genres
		genreGroup.MapGet("/", async (IGenreService genreService) =>
		{
			var allGenres = await genreService.GetAllAsync();
			return Results.Ok(allGenres.Select(genre => genre.ToGenreDto()));
		});

		// --------------------------------------------------
		// GET /genres/1

		// --------------------------------------------------
		// POST /genres

		// --------------------------------------------------
		// PUT /genres/1

		// --------------------------------------------------
		// DELETE /genres/1

		// --------------------------------------------------
		// DELETE /genres?ids=1,2,3
	}
}
