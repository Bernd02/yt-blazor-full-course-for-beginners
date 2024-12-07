﻿using GameStore.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Extensions;

public static class WebApplicationExtensions
{
	/// <summary>
	/// Method for migrating db on startup.
	/// </summary>
	public static async Task MigrateDbAsync(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreDbContext>();
		await dbContext.Database.MigrateAsync();
	}
}
