﻿using GameStore.Contracts.Game;
using GameStore.Services.Extentsions;
using GameStore.Services.GameService;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
	public const string ENDPOINT_GET_GAME = "GetGame";


	public static void MapGamesEndpoints(this WebApplication app)
	{
		var gamesGroup = app.MapGroup(Constants.GAMES_RESOURCE_NAME);


		// --------------------------------------------------
		// GET /games
		gamesGroup.MapGet("/", async (IGameService gameService) =>
		{
			return Results.Ok((await gameService.GetallAsync())
				.Select(game => game.ToGameSummaryDto()));
		});

		// --------------------------------------------------
		// GET /games/1
		gamesGroup.MapGet("/{id}", async (int id, IGameService gameService) =>
		{
			var game = await gameService.GetByIdAsync(id);
			if (null == game)
			{
				return Results.NotFound();
			}

			return Results.Ok(game.ToGameDetailsDto());
		})
		// .WithName defines a name for the endpoint
		.WithName(ENDPOINT_GET_GAME);

		// --------------------------------------------------
		// POST /games
		gamesGroup.MapPost("/", async (CreateGameDto createGameDto, IGameService gameService) =>
		{
			var game = createGameDto.ToGame();

			await gameService.UpsertAsync(game);

			// get fresh load of game inculding genre-data
			game = await gameService.GetByIdAsync(game.Id);

			return Results.CreatedAtRoute(
				ENDPOINT_GET_GAME,
				new { id = game!.Id },
				game.ToGameSummaryDto());
		});

		// --------------------------------------------------
		// PUT /games/1
		gamesGroup.MapPut("/{id}", async (int id, UpdateGameDto updateGameDto, IGameService gameService) =>
		{
			var existingGame = await gameService.GetByIdAsync(id);
			if (null == existingGame)
			{
				// tutor prefers to not do and "upsert" but rather return notFound
				// becuase dB with autoIncrementing ids create confusion when adding a new object
				// with a different Id when an upsert call is executed with a specific Id.
				return Results.NotFound();
			}

			var updatedGame = updateGameDto.ToGame(id);

			await gameService.UpsertAsync(updatedGame);

			return Results.NoContent();
		});

		// --------------------------------------------------
		// DELETE /games/1
		gamesGroup.MapDelete("/{id}", async (int id, IGameService gameService) =>
		{
			await gameService.DeleteAsync(id);

			return Results.NoContent();
		});

		// --------------------------------------------------
		// DELETE /games?ids=1,2,3
		gamesGroup.MapDelete("/", async ([FromQuery(Name = nameof(ids))] string ids, IGameService gameService) =>
		{
			var idsList = ids.Split(",")
				.Select(id => int.TryParse(id, out var parsedId) ? parsedId : (int?)null)
				.Where(parsedId => parsedId.HasValue)
				.Cast<int>()
				.ToList();

			await gameService.DelteManyAsync(idsList);

			return Results.NoContent();
		});
	}
}
