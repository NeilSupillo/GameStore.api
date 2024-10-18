
using GameStore.api.Data;
using GameStore.api.Dtos;
using GameStore.api.Entities;
using GameStore.api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.api.Endpoints;

public static class GamesEndpoints
{
  private static List<GameSummaryDto> games = [
     new (1, "Game 1", "Genre 1", 10.99m, new DateOnly(1991,3,10)),
    new (2, "Game 2", "Genre 2", 15.99m, new DateOnly(1991,3,5)),
    new (3, "Game 3", "Genre 3", 20.99m, new DateOnly(1991,3,2))
   ];

  public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
  {

    const string GetGameEndpointName = "GetGame";
    var group = app.MapGroup("games").WithParameterValidation();

    group.MapGet("/", async (GameStoreContext dbContext) =>
      await dbContext.Games
        .Include(game => game.Genre)
        .Select(game => game.ToGameSummaryDto())
        .AsNoTracking()
        .ToListAsync());



    group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
    {

      Game? game = await dbContext.Games.FindAsync(id);

      return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());

    }).WithName(GetGameEndpointName);

    group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
    {

      Game game = newGame.ToEntity();
      //game.Genre = dbContext.Genres.Find(newGame.GenreId);

      dbContext.Games.Add(game);
      await dbContext.SaveChangesAsync();


      return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());

    });

    group.MapPut("/{id}", async (int id, UpdateGameDto updateGame, GameStoreContext dbContext) =>
    {
      var existingGame = await dbContext.Games.FindAsync(id);

      if (existingGame is null)
      {
        return Results.NotFound();  // Return 404 if game is not found
      }
      dbContext.Entry(existingGame).CurrentValues.SetValues(updateGame.ToEntity(id));

      await dbContext.SaveChangesAsync();
      return Results.NoContent();
    });


    group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
    {

      //games.RemoveAll(game => game.Id == id);
      await dbContext.Games
        .Where(game => game.Id == id)
        .ExecuteDeleteAsync();

      return Results.NoContent();  // Return 204 if game is deleted 
    });
    return group;
  }
}

